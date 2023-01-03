using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;
using UnityEngine;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] Transform target;
        [SerializeField] ParticleSystem muzzleFlash;
        [SerializeField] float range;
        [SerializeField] float damage;
        [SerializeField] GameObject hitEffect;
        [SerializeField] AudioClip shooting;
        [SerializeField] Camera fpsCamera;


        [SerializeField] float shootingRange;
        Coroutine trigger;
        LightingSettings settings;
        bool check;
        public Health targetPlayer;
        Health health;
        Vector3 distanceToPlayer;


        bool running;


        void Start()
        {
            health = GetComponent<Health>();
            check = false;
            running = false;
        }

        void Update()
        {

            LookAtPlayer();

            if (target == null) return;

            if (!GetIsInRange())
            {

                GetComponent<Mover>().MoveTo(target.position);

            }

            else
            {
                GetComponent<Mover>().Cancel();

                if (health.IsDead())
                {
                    StopCoroutine(trigger);

                }

                else
                {
                    AttackBehaviour();
                }

            }
        }

        private void LookAtPlayer()
        {
            if (Vector3.Distance(transform.position, target.position) > 3.0f)
            {
                transform.LookAt(target.transform);
            }

        }

        private bool GetIsInRange()
        {

            return Vector3.Distance(transform.position, target.position) < weaponRange;

        }

        private bool AttackingRange()
        {

            return Vector3.Distance(transform.position, target.position) < shootingRange;

        }

        public bool CanAttack(GameObject combatTarget)
        {

            if (combatTarget == null) { return false; }
            targetPlayer = combatTarget.GetComponent<Health>();
            return targetPlayer != null && !targetPlayer.IsDead();
        }

        private void AttackBehaviour()
        {

            if (running == false)
            {
                trigger = StartCoroutine(TriggerAttack());
            }

        }

        public void CombatMode()
        {

        }

        IEnumerator TriggerAttack()
        {
            running = true;

            do
            {
                yield return new WaitForSeconds(0.5f);
                PlayMuzzleFlash();
                Process();

            } while (GetIsInRange());

            running = false;

        }


        private void PlayMuzzleFlash()
        {
            muzzleFlash.Play();
            AudioSource.PlayClipAtPoint(shooting, Camera.main.transform.position, 0.25f);
        }

        private void Process()
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
            {

                targetPlayer = hit.transform.GetComponent<Health>();
                if (targetPlayer == null) return;
                targetPlayer.PlayerTakeDamage(damage);
            }

            else

            {
                return;
            }
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

