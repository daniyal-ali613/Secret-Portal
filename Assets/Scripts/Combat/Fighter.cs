using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using RPG.Core;
using UnityEngine;
using System;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public float weaponRange = 2f;
        public Transform target;
        public ParticleSystem muzzleFlash;
        public float damage;
        public AudioClip shooting;
        public GameObject weapon;


        public float rotationSpeed = 10f;
        public float minimumAngle = -30f;
        public float maximumAngle = 30f;

        private float currentDistance;

        private Vector3 lastTargetPosition;



        public float shootingRange;
        Coroutine trigger;
        LightingSettings settings;
        bool check;
        public Health targetPlayer;
        Health health;

        Vector3 distanceToPlayer;

        NavMeshAgent navMesh;

        ActionSchedular actionSchedular;

        Collider coll;

        Mover mover;

        LevelLoader level;

        private Animator animator;



        bool running;
        bool looking;


        void Start()
        {
            health = GetComponent<Health>();
            check = false;
            running = false;
            level = FindObjectOfType<LevelLoader>();
            looking = true;
            mover = GetComponent<Mover>();
            Time.timeScale = 1;
            navMesh = GetComponent<NavMeshAgent>();
            actionSchedular = GetComponent<ActionSchedular>();
            coll = GetComponent<Collider>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (target == null) return;



            if (GetIsInRange() || health.CheckDamage() == true)
            {
                if (!health.IsDead())
                {
                    navMesh.enabled = true;
                }

                if (navMesh.enabled == true)
                {
                    mover.MoveTo(target.position);
                    OnPlayerVisibility();
                    if (looking == true && !navMesh.isStopped)
                    {
                        LookAtPlayer();
                    }
                }


            }

            else
            {

                navMesh.enabled = false;
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



        private bool GetIsInRange()
        {

            currentDistance = Vector3.Distance(transform.position, target.position);



            return currentDistance < weaponRange;


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
            bool raycastSuccess = Physics.Raycast(this.transform.position, this.transform.forward, out hit, shootingRange);
            if (!raycastSuccess) return;

            targetPlayer = hit.transform.GetComponent<Health>();
            if (targetPlayer == null) return;
            targetPlayer.PlayerTakeDamage(damage);
        }



        public void Attack(GameObject combatTarget)
        {
            actionSchedular.StartAction(this);
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
                animator.SetLayerWeight(1, 0);
                if (trigger != null)
                {
                    StopCoroutine(trigger);

                }

                coll.enabled = false;
                target = null;
                looking = false;
                navMesh.enabled = false;
            }

            if (target == null) return;



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

                else
                {

                    return;
                }
            }

        }
    }
}




