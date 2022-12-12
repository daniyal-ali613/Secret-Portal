using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {

            //navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {

            GetComponent<NavMeshAgent>().destination = destination;
            navMeshAgent.isStopped = false;

        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {

            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponentInChildren<Animator>().SetFloat("forwardSpeed", speed);

        }




    }

}


