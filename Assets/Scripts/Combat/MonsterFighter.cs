using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;


namespace RPG.Combat
{
    public class MonsterFighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] Transform target;
        [SerializeField] float range;
        [SerializeField] float damage;
        Coroutine trigger;
        LightingSettings settings;
        bool check;
        public Health targetPlayer;
        Health health;

        bool running;


        void Start()
        {
            health = GetComponent<Health>();
            check = false;
            running = false;

        }

        void Update()
        {

            if (target == null) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);

            }

            else
            {
                if (!health.IsDead())
                {
                    AttackBehaviour();
                }

                GetComponent<Mover>().Cancel();
            }
        }

        private bool GetIsInRange()
        {

            return Vector3.Distance(transform.position, target.position) < weaponRange;

        }

        public bool CanAttack(GameObject combatTarget)
        {

            if (combatTarget == null) { return false; }
            targetPlayer = combatTarget.GetComponent<Health>();
            return targetPlayer != null && !targetPlayer.IsDead();
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);


            if (running == false)
            {
                Debug.Log("Started");
                trigger = StartCoroutine(TriggerAttack());

            }


        }

        IEnumerator TriggerAttack()
        {
            running = true;

            do
            {
                yield return new WaitForSeconds(0.5f);
                Trigger();
            } while (GetIsInRange());

            running = false;

            Debug.Log("Ended");
        }




        private void Trigger()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }


        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.transform;
        }



        public void Cancel()
        {
            target = null;
        }
    }

}

