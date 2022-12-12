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
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0) {

            PlayMuzzleFlash();
            Process();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void Process()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {

            Debug.Log("I hit this thing: " + hit.transform.name);

            Health target = hit.transform.GetComponent<Health>();
            if (target == null) return;
            target.EnemyTakeDamage(damage);
        }

        else

        {
            return;
        }
    }
}
