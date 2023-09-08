using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] Transform tracerOrigin;
    [SerializeField] AudioClip shooting;
    [SerializeField] AudioClip empty;



    public Health PlayerHealth;
    public Health EnemyHealth;


    public List<GameObject> vfx = new List<GameObject>();
    Ammo ammo;

    private GameObject effectToSpawn;
    Animator animator;
    Transform cachedTransform;
    Transform mainCameraTransform;

    void Start()
    {
        ammo = GetComponent<Ammo>();
        animator = GetComponent<Animator>();
        effectToSpawn = vfx[0];
        cachedTransform = transform;
        mainCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!PlayerHealth.IsDead() && !EnemyHealth.IsDead())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (ammo.GetCurrentAmmount() <= 0)
                {
                    AudioSource.PlayClipAtPoint(empty, mainCameraTransform.position);
                    return;
                }
                SpawnVFX(effectToSpawn, tracerOrigin, ammo);
                AudioSource.PlayClipAtPoint(shooting, mainCameraTransform.position, 0.25f);
                animator.SetTrigger("shoot");
            }

        }

    }

    private static void SpawnVFX(GameObject effectToSpawn, Transform tracerOrigin, Ammo ammo)
    {
        GameObject vfx;
        if (tracerOrigin != null)
        {
            vfx = Instantiate(effectToSpawn, tracerOrigin.position, tracerOrigin.rotation);
            ammo.ReduceCurrentAmmo(1);
        }
    }
}
