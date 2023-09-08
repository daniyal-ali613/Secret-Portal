using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] GameObject gameLostText;
        [SerializeField] float healthPoints = 100f;

        private CursorHandler cursorHandler;

        DamageIndicator damageIndicator;

        [SerializeField] AudioClip wooshSound;
        bool isDead = false;
        float currentHealth;

        public LevelLoader level;

        float addition;

        bool getHit;


        private void Awake()
        {
            addition = 10f;
            damageIndicator = FindObjectOfType<DamageIndicator>();
            cursorHandler = FindObjectOfType<CursorHandler>();
            this.getHit = false;
            addition = 30;
        }

        private void Update()
        {
            currentHealth = this.healthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }


        public void EnemyTakeDamage(float damage)
        {
            this.healthPoints = Mathf.Max(this.healthPoints - damage, 0);

            if (this.getHit != true)
            {
                this.getHit = true;
            }

            if (this.healthPoints <= 0)
            {

                EnemyDie();
            }
        }

        private void EnemyDie()
        {
            level.GetComponent<LevelLoader>().RemoveEnemy(gameObject);
            if (isDead) return;
            isDead = true;
            GetComponent<ActionSchedular>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("Die");
        }


        public void PlayerTakeDamage(float damage, GameObject enemy)
        {
            Vector3 direction = transform.position - enemy.transform.position;
            this.healthPoints = Mathf.Max(this.healthPoints - damage, 0);
            damageIndicator.ShowDamageIndicator(direction);

            if (this.healthPoints <= 0)
            {
                StartCoroutine(PlayerDie());
            }
        }

        public void PlayerGotHealth()
        {
            healthPoints = Mathf.Clamp(healthPoints + addition, 0f, 100f);

        }


        IEnumerator PlayerDie()
        {
            if (isDead) yield return null;
            isDead = true;
            yield return new WaitForSeconds(1);
            RenderSettings.ambientLight = Color.red;
            AudioSource.PlayClipAtPoint(wooshSound, Camera.main.transform.position);
            cursorHandler.UnLockCursor();
            gameLostText.SetActive(true);
            Time.timeScale = 0;
        }

        public float GetHealth()
        {
            return healthPoints;
        }

        public bool CheckDamage()
        {
            return this.getHit;

        }


    }

}










