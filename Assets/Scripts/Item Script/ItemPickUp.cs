using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    public AudioClip pickUpSound;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
        if (!ItemData)
        {
            ItemData = ScriptableObject.CreateInstance<InventoryItemData>();
            ItemData.WeaponType = this.gameObject.name;

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