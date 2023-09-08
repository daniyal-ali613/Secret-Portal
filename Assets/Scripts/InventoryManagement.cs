using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public GameObject inventoryUI;
    bool inventory;

    public AudioClip inventorySound;
    void Start()
    {
        inventoryUI.SetActive(false);
        inventory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.activeInHierarchy == false)
            {
                inventory = true;
                inventoryUI.SetActive(true);
                AudioSource.PlayClipAtPoint(inventorySound, Camera.main.transform.position);
            }

            else
            {
                inventory = false;
                inventoryUI.SetActive(false);
                AudioSource.PlayClipAtPoint(inventorySound, Camera.main.transform.position);
            }
        }
    }
}
