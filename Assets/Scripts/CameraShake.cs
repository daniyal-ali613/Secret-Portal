using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.7f;
    public float dampingSpeed = 1.0f;
    public Vector3 shakeDirection = Vector3.right;

    public GameObject weapon;

    Vector3 initialPosition;
    Vector3 weaponInitialPosition;


    void OnEnable()
    {
        initialPosition = transform.localPosition;
        weaponInitialPosition = weapon.transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            Vector3 shake = shakeDirection * shakeMagnitude;
            shake = shake + Random.insideUnitSphere * shakeMagnitude * 0.1f;
            transform.localPosition = initialPosition + shake;
            weapon.transform.localPosition = weaponInitialPosition + shake;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
            weapon.transform.localPosition = weaponInitialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.5f;
    }

}
