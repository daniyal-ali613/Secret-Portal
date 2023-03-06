using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using UnityEngine.AI;


namespace RPG.Combat
{
    public class MonsterFighter : MonoBehaviour, IAction
    {
        public float weaponRange = 2f;

        public Transform target;
        public float range;
        public float damage;

        public GameObject shoot;
        public AudioClip punchSound;
        public AudioClip hurtSound;
        public AudioClip runSound;


        public AudioClip roar;

        public float punchRange = 1f;

        public GameObject again;


        public CameraShake cameraShake;

        public float rotationSpeed = 10f;
        public float minimumAngle = -30f;
        public float maximumAngle = 30f;
        public Animator playerAnimator;

        public GameObject mutantParticles;
        LightingSettings settings;
        Animator animator;
        bool check;
        public Health targetPlayer;

        public AudioSource background;
        Health health;

        Collider coll;

        NavMeshAgent navMesh;

        float timer;

        bool running;
        bool looking;
        bool moving;

        Rigidbody rb;

        Quaternion lookRotation;


        void Start()
        {
            timer = 0;
            health = GetComponent<Health>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            check = false;
            running = false;
            Time.timeScale = 1;
            coll = GetComponent<Collider>();
            navMesh = GetComponent<NavMeshAgent>();
            looking = true;
            background.Stop();
            moving = false;
        }

        void Update()
        {

            if (target == null) return;

            CheckHealth();

            if (!GetIsInRange())
            {
                if (navMesh.enabled == true)
                {
                    GetComponent<Mover>().MoveTo(target.position);
                    moving = true;
                }
            }

            else
            {
                if (!health.IsDead())
                {
                    moving = false;
                    GetComponent<Mover>().Cancel();
                    AttackBehaviour();
                    navMesh.enabled = true;
                }

            }

            if (looking == true)
            {
                LookAtPlayer();
            }


            timer += Time.deltaTime;
            if (timer >= 6 && moving == true)
            {
                Roar();
            }
        }

        private void CheckHealth()
        {
            if (health.IsDead())
            {
                timer = 0;
                coll.enabled = false;
                navMesh.enabled = false;
                looking = false;

                StartCoroutine(Restart());

            }

        }

        private void LookAtPlayer()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, transform.forward);
            angle = Mathf.Clamp(angle, minimumAngle, maximumAngle);

            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        }

        private void Roar()
        {
            if (!health.IsDead())
            {
                timer = 0;

                HitPlayer();


                AudioSource.PlayClipAtPoint(roar, Camera.main.transform.position);
            }

        }

        private void HitPlayer()
        {
            var hitEffect = Instantiate(mutantParticles, shoot.transform.position, transform.rotation = lookRotation);
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
                target.GetComponent<Health>().PlayerTakeDamage(damage, this.gameObject);
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

        IEnumerator Restart()
        {

            yield return new WaitForSeconds(2);
            again.SetActive(true);
            Time.timeScale = 0;
            background.Play();

        }

    }

}

