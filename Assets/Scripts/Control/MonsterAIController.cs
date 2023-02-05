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
        NavMeshAgent navMeshAgent;
        MonsterFighter fighter;
        Health health;
        GameObject player;
        Vector3 guardPosition;
        Mover mover;


        void Start()
        {
            fighter = GetComponent<MonsterFighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }




        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {

                AttackBehaviour();

            }

            if (health.IsDead())
            {

                GetComponent<Rigidbody>().isKinematic = true;
            }

        }



        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
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




