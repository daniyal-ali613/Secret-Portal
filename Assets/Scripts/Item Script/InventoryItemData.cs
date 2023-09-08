using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    // Start is called before the first frame update
    public Sprite Icon;
    public int MaxStackSize = 1;
    public int InventorySize;
    public string DisplayName;
    [TextArea(4, 4)] public string Description;

    public int RemainAmmo = 0;
    public string WeaponType;

    public string WeaponId;


}