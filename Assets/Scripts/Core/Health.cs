using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] GameObject gameLostText;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }


        public void PlayerTakeDamage(float damage)
        {
            this.healthPoints = Mathf.Max(this.healthPoints - damage, 0);
            if (this.healthPoints <= 0)
            {
                StartCoroutine(PlayerDie());
            }
        }

        public void EnemyTakeDamage(float damage)
        {
            this.healthPoints = Mathf.Max(this.healthPoints - damage, 0);
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
        }

        IEnumerator PlayerDie()
        {
            if (isDead) yield return null;
            isDead = true;
            yield return new WaitForSeconds(1);
            gameLostText.SetActive(true);
            RenderSettings.ambientLight = Color.red;


            Time.timeScale = 0;
        }

        public float GetPlayerHealth()
        {
            return healthPoints;
        }

    }





}






