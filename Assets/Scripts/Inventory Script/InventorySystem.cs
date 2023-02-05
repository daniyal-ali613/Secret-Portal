using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;

    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amount) // add to slot
    {
        if (itemToAdd.Icon == null)
        {
            if (itemToAdd.WeaponType.Equals("arm4"))
            {
                itemToAdd.Icon = Resources.Load<Sprite>("arm4");
            }
            else if (itemToAdd.WeaponType.Equals("Futuristic_Weapon_Lowpoly74_2"))
            {
                itemToAdd.Icon = Resources.Load<Sprite>("Futuristic_Weapon_Lowpoly74_2");
            }
            else if (itemToAdd.WeaponType.Equals("sniper"))
            {
                itemToAdd.Icon = Resources.Load<Sprite>("sniper");
            }
            else if (itemToAdd.WeaponType.Equals("untitled"))
            {
                itemToAdd.Icon = Resources.Load<Sprite>("untitled");
            }
            else
            {
                itemToAdd.Icon = Resources.Load<Sprite>("temp_AK47");
            }
        }

        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amount))
                {
                    slot.AddToStack(amount);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }

        }
        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amount);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    //drop this weapon, return first valid weapon
    public int dropThisWeapon(int currentWeapon)
    {
        InventorySlots[currentWeapon].ClearSlot();
        for (int i = 0; i < 4; i++)
        {
            if (InventorySlots[i].ItemData != null)
            {
                return i;
            }
        }

        return 0;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
}