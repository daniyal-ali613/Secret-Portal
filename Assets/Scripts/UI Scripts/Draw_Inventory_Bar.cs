using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Draw_Inventory_Bar : MonoBehaviour
{
    private InventorySystem inventorySystem;
    private List<InventorySlot> inventorySlots;
    private int currentWeapon = 0;
    private List<Sprite> sprites;

    public Image p1;
    public Image p2;
    public Image p3;
    public Image p4;


    private void Start()
    {
        inventorySystem = GameObject.FindWithTag("Player").GetComponent<InventoryHolder>().InventorySystem;
        inventorySlots = inventorySystem.InventorySlots;
        sprites = new List<Sprite>(4) { null, null, null, null };
        UpdateSprite();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 4;
        }
        UpdateSprite();

    }

    private void UpdateSprite()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ItemData == null)
            {
                sprites[i] = null;
                continue;
            }
            sprites[i] = inventorySlots[i].ItemData.Icon;
        }

        p1.sprite = sprites[0];
        p2.sprite = sprites[1];
        p3.sprite = sprites[2];
        p4.sprite = sprites[3];
    }

    private void UpdateSprite(int slot)
    {
        sprites[slot] = inventorySlots[slot].ItemData.Icon;

    }
    public int GetCurrentWeapon()
    {
        return currentWeapon;
    }


}