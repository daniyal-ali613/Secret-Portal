using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoText : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    int currentAmmount;


    void Update()
    {
        DisplayText();
    }

    private void DisplayText()
    {

        foreach (var weapon in weapons)
        {

            if (weapon.activeInHierarchy)
            {
                currentAmmount = weapon.GetComponent<Ammo>().GetCurrentAmmount();
                GetComponent<TextMeshProUGUI>().text = currentAmmount.ToString();
            }
        }

    }

}