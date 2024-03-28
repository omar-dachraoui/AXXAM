using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // Flag to determine if the player is in range
    public bool playerinrange;

    // Name of the item associated with the interactable object
    public string ItemName;

    [SerializeField] private bool rune;
    
    public event EventHandler OnRuneDetected;

    [SerializeField]AudioSource audioSource;

    // Method to get the name of the item
    public string GetItemName()
    {
        return ItemName;
    }



    

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the 'E' key, is in range, the object is targeted, and it's the selected object
        if (Input.GetKeyDown(KeyCode.E) && playerinrange && SelectionManager.Instance.ontarget && SelectionManager.Instance.SelectedObject == gameObject)
        {
            if(rune)
            {
                if(audioSource != null)
                {
                    audioSource.Play();
                    OnRuneDetected?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
               // Check if the inventory is not full
                if (InventorySystem.Instance.CheckSlotAvailable(1))
                {
                    // Add the item to the inventory and destroy the interactable object
                 InventorySystem.Instance.AddToInventory(ItemName);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory is full.");
                }
            }
        }
    }

    // Called when another collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player
        if (other.CompareTag("Player"))
        {
            // Set player in range to true
            playerinrange = true;
            
        }
    }

    // Called when another collider exits the trigger zone
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is the player
        if (other.CompareTag("Player"))
        {
            // Set player in range to false
            playerinrange = false;
        }
    }
}
