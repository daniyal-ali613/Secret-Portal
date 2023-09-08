using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class FootStepSound : MonoBehaviour
{
    public AudioSource source;
    public AudioSource breathing;

    [SerializeField] Health monsterHealth;

    Health health;

    private bool hasStarted = false;


    float speed;
    Rigidbody rigid;



    private void Start()
    {
        hasStarted = true;

        source = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        health = GetComponent<Health>();

    }


    void Update()
    {
        FootSound();
        breathingSound();
    }


    private void FootSound()
    {
        speed = rigid.velocity.magnitude;


        if (speed > 0.1)
        {
            if (!hasStarted)
            {
                source.Play();
                hasStarted = true;
            }
        }

        if (speed < 0.1 || Time.timeScale == 0)
        {
            source.Stop();
            hasStarted = false;
        }


    }

    private void breathingSound()
    {

        if (health.GetHealth() <= 30)
        {
            if (!breathing.isPlaying)
            {

                breathing.Play();
                //Camera.main.GetComponent<Dizziness>().StartDiziness();
            }


        }

        if (health.GetHealth() >= 30 || Time.timeScale == 0)
        {
            breathing.Stop();
        }

    }

}


