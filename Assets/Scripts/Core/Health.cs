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

        [SerializeField] AudioClip wooshSound;
        bool isDead = false;
        float currentHealth;
        [SerializeField] float addition;

        LevelLoader level;
        bool getHit;


        private void Awake()
        {
            addition = 10f;
            level = FindObjectOfType<LevelLoader>();
            this.getHit = false;
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
            if (isDead) return;
            isDead = true;
            GetComponent<ActionSchedular>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("Die");
            level.RemoveEnemy(this.gameObject);
        }


        public void PlayerTakeDamage(float damage)
        {
            this.healthPoints = Mathf.Max(this.healthPoints - damage, 0);
            if (this.healthPoints <= 0)
            {
                StartCoroutine(PlayerDie());
            }
        }

        public void PlayerGotHealth()
        {
            this.healthPoints = this.healthPoints + addition;
        }

        IEnumerator PlayerDie()
        {
            if (isDead) yield return null;
            isDead = true;
            yield return new WaitForSeconds(1);
            gameLostText.SetActive(true);
            RenderSettings.ambientLight = Color.red;
            AudioSource.PlayClipAtPoint(wooshSound, Camera.main.transform.position);


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






