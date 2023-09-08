using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using RPG.Core;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] Transform tracerOrigin;
    [SerializeField] AudioClip shooting;
    [SerializeField] AudioClip empty;

    [SerializeField] GameObject pauseCanvas;
    public string WeaponId;


    public Health PlayerHealth;
    public Health EnemyHealth;

    [SerializeField] LevelLoader level;

    public List<GameObject> vfx = new List<GameObject>();
    Ammo ammo;

    private GameObject effectToSpawn;
    Animator animator;
    Transform cachedTransform;

    Transform mainCameraTransform;


    float yRotation;

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

        if (SceneManager.GetActiveScene().name == "Level")
        {
            if (Time.timeScale != 0)
            {
                ShootMode();
            }

        }

        else if (SceneManager.GetActiveScene().name == "Boss")
        {

            if (Time.timeScale != 0)
            {
                ShootMode();
            }

        }

    }

    private void ShootMode()
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
