using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoText : MonoBehaviour
{
    List<GameObject> weapons = new List<GameObject>();

    void Update()
    {
        foreach (var weapon in weapons)
        {
            if (weapon.activeInHierarchy == true)
            {

                //weapon.GetComponent<>().GetCurrentAmmo();
            }
        }
    }
}
