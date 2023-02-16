using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    private InventoryItemData currentWeaponData;
    private string currentWeaponName;
    private InventoryHolder inventoryHolder;

    void Start()
    {
        SetWeaponActive();
    }

    private void SetWeaponActive()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        inventoryHolder = player.GetComponent<InventoryHolder>();
        currentWeaponData = inventoryHolder.getWeapon(currentWeapon);

        if (currentWeaponData != null)
        {
            currentWeaponName = currentWeaponData.WeaponType;
        }

        int weaponIndex = 0;
        foreach (Transform weapon in transform)
        {
            if (weapon.name == currentWeaponName)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            weaponIndex++;
        }



    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheelInput();

        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }

    }

    private void ProcessScrollWheelInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentWeapon = 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentWeapon = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentWeapon = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            currentWeapon = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentWeapon = 7;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentWeapon = 8;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentWeapon = 9;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            currentWeapon = inventoryHolder.dropCurrentWeapon(currentWeapon);
        }


    }

}
