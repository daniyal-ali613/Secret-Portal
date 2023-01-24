using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    AudioSource source;
    private bool hasStarted = false;


    float speed;
    Rigidbody rigid;



    private void Start()
    {
        hasStarted = true;

        source = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();

    }


    void Update()
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

        if (speed < 0.1)
        {
            source.Stop();
            hasStarted = false;

        }

        Debug.Log(speed);
    }
}
