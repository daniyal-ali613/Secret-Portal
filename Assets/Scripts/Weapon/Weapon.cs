using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] int fireRate = 25;

    [SerializeField] TrailRenderer laserTracer;

    [SerializeField] Transform tracerOrigin;
    [SerializeField] GameObject hitEffect;
    Ammo ammo;
    [SerializeField] AudioClip shooting;
    [SerializeField] AudioClip empty;

    float cameraDistanceOffset;

    Ray ray;

    private bool attacked;

    GameObject health;
    Animator animator;




    void Start()
    {
        ammo = GetComponent<Ammo>();
        health = GameObject.FindGameObjectWithTag("Player");
        attacked = false;
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (ammo.GetCurrentAmmount() <= 0)
            {
                AudioSource.PlayClipAtPoint(empty, Camera.main.transform.position);
            }

            else
            {
                Shoot();
                AudioSource.PlayClipAtPoint(shooting, Camera.main.transform.position);
                animator.SetTrigger("shoot");
            }

        }

        Debug.DrawRay(transform.position, transform.forward, Color.red, range);
        cameraDistanceOffset = Vector3.Distance(FPCamera.transform.position, this.transform.position);


    }

    private void Shoot()
    {

        if (health.GetComponent<Health>().IsDead()) return;

        else
        {

            PlayMuzzleFlash();
            Process();
            ammo.ReduceCurrentAmmo(1);

        }


    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void Process()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward * cameraDistanceOffset, out hit, range))
        {
            attacked = true;

            var tracer = Instantiate(laserTracer, FPCamera.transform.position, Quaternion.identity);
            tracer.AddPosition(FPCamera.transform.position);

            tracer.transform.position = hit.point;

            CreateHitImpact(hit);


            Health target = hit.transform.GetComponent<Health>();
            if (target == null) return;
            target.EnemyTakeDamage(damage);
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

    public bool AttackIndicator()
    {
        return attacked;
    }
}
