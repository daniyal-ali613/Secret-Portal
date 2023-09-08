using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class HealthPickUp : MonoBehaviour
    {
        [SerializeField] AudioClip pickUpSound;
        public float amplitude = 1.0f; // the amplitude of the motion
        public float speed = 1.0f; // the speed of the motion
        private Vector3 startPos;
        public Health playerHealth;



        void Start()
        {
            startPos = transform.position;
        }

        void Update()
        {
            float y = amplitude * Mathf.Sin(speed * Time.time);
            transform.position = startPos + new Vector3(0, y, 0);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);
                playerHealth.PlayerGotHealth();
                gameObject.SetActive(false);
            }
        }
    }
}

