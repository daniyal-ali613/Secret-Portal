using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class EnemyDropWeapon : MonoBehaviour
    {
        public GameObject weapon;

        private Health health;
        float timer;

        private void Start()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead())
            {
                DropWeapon();
            }

            timer = Time.timeScale;
        }

        private void DropWeapon()
        {
            // Detach the weapon object from the enemy object
            weapon.transform.parent = null;


            // Add physics to the weapon object
            weapon.AddComponent<Rigidbody>();

            // Apply force to the weapon object so it falls to the floor
            weapon.GetComponent<Rigidbody>().AddForce(Vector3.down * 5f * Time.deltaTime, ForceMode.Impulse);

            if (timer > 1f)
            {
                weapon.GetComponent<Rigidbody>().isKinematic = true;

            }
        }
    }
}

