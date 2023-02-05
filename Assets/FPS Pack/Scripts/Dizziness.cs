using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dizziness : MonoBehaviour
{
    private float shakeDuration = 0f;
    private float shakeIntensity = 0.7f;
    private float shakeStartTime;



    private Vector3 originalPos;

    void Update()
    {

        if (shakeDuration > 0)
        {
            float elapsed = Time.time - shakeStartTime;
            float percentComplete = elapsed / shakeDuration;

            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= shakeIntensity * damper;
            y *= shakeIntensity * damper;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            if (percentComplete >= 1.0f)
            {
                shakeDuration = 0f;
                transform.localPosition = originalPos;
            }
        }
    }

    public void StartDiziness()
    {
        shakeDuration = 1f;
        shakeStartTime = Time.time;
        originalPos = transform.localPosition;
    }
}
