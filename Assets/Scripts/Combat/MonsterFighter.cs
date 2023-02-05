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
        [SerializeField] AudioClip punchSound;
        [SerializeField] AudioClip hurtSound;
        [SerializeField] AudioClip runSound;

        [SerializeField] AudioClip roar;

        [SerializeField] float punchRange = 1f;

        [SerializeField] GameObject again;


        public CameraShake cameraShake;

        public float rotationSpeed = 10f;
        public float minimumAngle = -30f;
        public float maximumAngle = 30f;

        public Animator playerAnimator;
        LightingSettings settings;
        Animator animator;
        bool check;
        public Health targetPlayer;
        Health health;

        float timer;

        bool running;


        void Start()
        {
            timer = 0;
            health = GetComponent<Health>();
            animator = GetComponent<Animator>();
            check = false;
            running = false;
            Time.timeScale = 1;
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

                else
                {
                    again.SetActive(true);
                }

                GetComponent<Mover>().Cancel();
            }

            LookAtPlayer();

            timer += Time.deltaTime;
            if (timer >= 6)
            {

                Roar();
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

        private void Roar()
        {
            timer = 0;
            AudioSource.PlayClipAtPoint(roar, Camera.main.transform.position);
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
            animator.SetTrigger("attack");

        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.transform;
        }

        public void OnPunch()
        {
            if (Vector3.Distance(transform.position, target.position) <= punchRange)
            {
                playerAnimator.SetTrigger("hurt");
                AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position);
                AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position);
                cameraShake.TriggerShake();
                target.GetComponent<Health>().PlayerTakeDamage(damage);
            }

        }

        public void Cancel()
        {
            target = null;
        }


        public void OnRun()
        {
            AudioSource.PlayClipAtPoint(runSound, Camera.main.transform.position);
        }


    }

}

