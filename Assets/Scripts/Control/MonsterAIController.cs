using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine.AI;


namespace RPG.Control
{
    public class MonsterAIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float WayPointDwellTime = 3f;
        [SerializeField] float WayPointTolerence = 1f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] AudioClip monsterRoar;
        NavMeshAgent navMeshAgent;
        MonsterFighter fighter;
        Health health;
        GameObject player;
        Vector3 guardPosition;
        Mover mover;
        int currentWayPointIndex = 0;

        float timeSinceArrivedAtWayPoint = Mathf.Infinity;

        void Start()
        {
            fighter = GetComponent<MonsterFighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            StartCoroutine(Growling());
        }




        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {

                AttackBehaviour();

            }

            else
            {

                PatrolBehaviour();
            }


            if (health.IsDead())
            {

                GetComponent<Rigidbody>().isKinematic = true;
            }

            UpdateTimers();

        }

        private void UpdateTimers()
        {
            timeSinceArrivedAtWayPoint += Time.deltaTime;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }



        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {

                    timeSinceArrivedAtWayPoint = 0;
                    CycleWayPoint();
                }

                nextPosition = GetCurrentWayPoint();
            }

            if (timeSinceArrivedAtWayPoint > WayPointDwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
        }

        private bool AtWayPoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < WayPointTolerence;
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWaypoint(currentWayPointIndex);
        }

        IEnumerator Growling()
        {
            do
            {
                yield return new WaitForSeconds(25);
                GetComponent<Animator>().ResetTrigger("growl");
                GetComponent<Animator>().SetTrigger("growl");
                AudioSource.PlayClipAtPoint(monsterRoar, Camera.main.transform.position);

            } while (health.IsDead());
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

    }
}




