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
        [SerializeField] GameObject weapon;

        public float rotationSpeed = 10f;
        public float minimumAngle = -30f;
        public float maximumAngle = 30f;



        [SerializeField] float shootingRange;
        Coroutine trigger;
        LightingSettings settings;
        bool check;
        public Health targetPlayer;
        Health health;
        Vector3 distanceToPlayer;

        LevelLoader level;



        bool running;
        bool looking;


        void Start()
        {
            health = GetComponent<Health>();
            check = false;
            running = false;
            level = FindObjectOfType<LevelLoader>();
            looking = true;
            Time.timeScale = 1;

        }

        void Update()
        {
            if (target == null) return;



            if (GetIsInRange())
            {

                GetComponent<Mover>().MoveTo(target.position);
                if (looking == true)
                {
                    LookAtPlayer();
                }

                OnPlayerVisibility();

            }


        }

        private void LookAtPlayer()
        {

            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, transform.forward);
            angle = Mathf.Clamp(angle, minimumAngle, maximumAngle);


            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        }

        public void WeaponIdle()
        {
            weapon.transform.localPosition = new Vector3(0.0599999987f, 1.48800004f, 0.379000008f);
        }

        public void WeaponMovement()
        {
            weapon.transform.localPosition = new Vector3(0.00100000005f, 1.39699996f, 0.558000028f);

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

            if (running == false)
            {
                trigger = StartCoroutine(TriggerAttack());
            }

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
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, shootingRange))
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

        private void OnPlayerVisibility()
        {

            if (health.IsDead())
            {
                StopCoroutine(trigger);
                GetComponent<Collider>().enabled = false;
                target = null;
                looking = false;
            }

            if (target == null) return;

            // check if the dot product of the direction to the player and the forward vector of the enemy is greater than 0
            // if it is, then the player is within the field of view of the enemy
            //Vector3 directionToPlayer = (target.position - transform.position).normalized;
            //float dotProduct = Vector3.Dot(directionToPlayer, transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, shootingRange))
            {
                // Player is in line of sight, start shooting

                if (hit.collider.gameObject.transform == target)
                {

                    AttackBehaviour();

                }
            }
            else
            {
                // Player is not in line of sight, stop shooting
                if (trigger != null)
                {
                    running = false;
                    StopCoroutine(trigger);
                }
            }

        }
    }
}




