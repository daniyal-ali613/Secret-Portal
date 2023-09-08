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
        Fighter fighter;
        Health health;
        GameObject player;
        Vector3 guardPosition;
        Mover mover;

        void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardPosition = transform.position;
            mover = GetComponent<Mover>();
        }


        private void Update()
        {

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }


            if (this.health.IsDead())
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


