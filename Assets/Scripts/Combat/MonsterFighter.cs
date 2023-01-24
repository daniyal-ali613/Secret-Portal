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
        public Animator playerAnimator;
        LightingSettings settings;
        Animator animator;
        bool check;
        public Health targetPlayer;
        Health health;
        bool collided;

        bool running;


        void Start()
        {
            health = GetComponent<Health>();
            animator = GetComponent<Animator>();
            check = false;
            running = false;
            collided = false;
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

            LookAtPlayer();
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
            if (collided == true)
            {
                playerAnimator.SetTrigger("hurt");
                AudioSource.PlayClipAtPoint(punchSound, Camera.main.transform.position);
                AudioSource.PlayClipAtPoint(hurtSound, Camera.main.transform.position);
                health.PlayerTakeDamage(1);
            }

        }

        public void Cancel()
        {
            target = null;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                collided = true;
            }

            else
            {
                collided = false;
            }

        }

        public void OnRun()
        {
            AudioSource.PlayClipAtPoint(runSound, Camera.main.transform.position);
        }
    }

}

