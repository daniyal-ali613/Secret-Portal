using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class EnemyDropWeapon : MonoBehaviour
    {
        public GameObject weapon;

        private Health health;

        Rigidbody weaponRigidbody;

        float timer;

        private void Start()
        {
            health = GetComponent<Health>();
            weaponRigidbody = weapon.GetComponent<Rigidbody>();

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


            if (!weaponRigidbody)
            {
                weaponRigidbody = weapon.AddComponent<Rigidbody>();
            }

            weaponRigidbody.velocity = Vector3.down * 10f;

        }



    }
}

