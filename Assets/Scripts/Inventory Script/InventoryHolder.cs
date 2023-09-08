using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize = 4;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
        InventoryItemData init_weapon_arm4 = ScriptableObject.CreateInstance<InventoryItemData>();
        init_weapon_arm4.WeaponId = "one";
        inventorySystem.AddToInventory(init_weapon_arm4, 1);
    }

    public InventoryItemData getWeapon(int i)
    {
        InventoryItemData itemData = inventorySystem.InventorySlots[i].ItemData;
        return itemData;
    }

    public int dropCurrentWeapon(int currentWeapon)
    {
        int temp = 0;
        foreach (InventorySlot slot in inventorySystem.InventorySlots)
        {
            if (slot.ItemData != null) temp++;
        }

        if (temp == 1)
        {
            return currentWeapon;
        }
        return inventorySystem.dropThisWeapon(currentWeapon);
    }
}