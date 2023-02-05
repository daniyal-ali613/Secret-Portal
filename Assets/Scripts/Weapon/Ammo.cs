using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammoAmount;

    public void ReduceCurrentAmmo(int ammo)
    {
        ammoAmount = ammoAmount - ammo;
    }

    public int GetCurrentAmmount()
    {

        return ammoAmount;
    }



}
