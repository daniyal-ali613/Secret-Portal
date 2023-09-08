using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{
    public float force = 5f;
    public float damage = 1f;
    GameObject impact;


    [SerializeField] GameObject hitEffect;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }




    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().PlayerTakeDamage(damage, this.gameObject);
            Destroy(this.gameObject);
        }

        else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().EnemyTakeDamage(damage);
            Vector3 destroyedObjectPosition = transform.position;
            Destroy(this.gameObject);
            Instantiate(hitEffect, destroyedObjectPosition, Quaternion.LookRotation(destroyedObjectPosition.normalized));
        }

    }


}
