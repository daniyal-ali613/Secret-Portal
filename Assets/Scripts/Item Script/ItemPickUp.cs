using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    public AudioClip pickUpSound;

    public SphereCollider myCollider;

    private void Awake()
    {
        if (!ItemData)
        {
            ItemData = ScriptableObject.CreateInstance<InventoryItemData>();
            ItemData.WeaponType = this.gameObject.transform.GetChild(0).name;

        }
    }

    private void OnTriggerEnter(Collider other) //should change to press a button
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();

        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 20f) * Time.deltaTime);
    }
}