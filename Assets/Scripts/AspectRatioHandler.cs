using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatioHandler : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        cam.aspect = aspectRatio;
    }
}
