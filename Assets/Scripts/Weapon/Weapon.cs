using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] AudioClip shooting;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            AudioSource.PlayClipAtPoint(shooting, Camera.main.transform.position);
        }
    }

    private void Shoot()
    {


        PlayMuzzleFlash();
        Process();
        ammoSlot.ReduceCurrentAmmo(ammoType);

    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void Process()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {

            Debug.Log("I hit this thing: " + hit.transform.name);

            CreateHitImpact(hit);


            Health target = hit.transform.GetComponent<Health>();
            if (target == null) return;
            target.EnemyTakeDamage(damage);
            target.GetComponent<Animator>().SetTrigger("damage");
        }

        else

        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.transform.position, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
