using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float WayPointDwellTime = 3f;
        [SerializeField] float WayPointTolerence = 1f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] List<GameObject> enemies = new List<GameObject>();
        int enemyCounter;
        NavMeshAgent navMeshAgent;
        Fighter fighter;
        Health health;
        GameObject player;
        Vector3 guardPosition;
        Mover mover;
        int currentWayPointIndex = 0;

        float timeSinceArrivedAtWayPoint = Mathf.Infinity;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyCounter = 0;
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
                enemyCounter++;
            }

            UpdateTimers();

            if (enemyCounter >= enemies.Count)
            {
                StartCoroutine(SceneChange());
            }
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        IEnumerator SceneChange()
        {
            yield return new WaitForSeconds(2);

        }

    }
}


