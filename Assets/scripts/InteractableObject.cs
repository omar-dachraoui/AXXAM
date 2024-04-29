using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour,IobjectInteractable
{
    // Flag to determine if the player is in range
    public bool playerinrange;

    // Name of the item associated with the interactable object
    public string ItemName;

    [SerializeField] private bool rune;
    
    public event EventHandler OnRuneDetected;

    AudioSource audioSource;

    // Method to get the name of the item
    // public string GetItemName()
    // {
    //     return ItemName;
    // }



    

  
    private void Start()
    {
        if(rune)
        {
            audioSource = GetComponent<AudioSource>();
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

    public void Interact()
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
                    SelectionManager.Instance.HandIcon.gameObject.SetActive(false);
                    SelectionManager.Instance.HandIsVisible = false;
                }
                else
                {
                    Debug.Log("Inventory is full.");
                }
            }
         

            
    }

    public void SetObjectRelatedUI()
    {
        // Check if the object has an InteractableObject component
            if (playerinrange)
            {
                SelectionManager.Instance.ontarget = true;
                SelectionManager.Instance.SelectedObject = this.gameObject;
                // Display the item name from the InteractableObject component
                SelectionManager.Instance.interaction_text.text = ItemName;

                // Show the interaction UI
                SelectionManager.Instance.interaction_Info_UI.SetActive(true);
                
                // Check if the object is pickable and adjust the icons accordingly
                if (CompareTag("pickable"))
                {
                    
                    SelectionManager.Instance.HandIcon.gameObject.SetActive(true);
                    SelectionManager.Instance.HandIsVisible = true;
                }
                else
                {
                    SelectionManager.Instance.HandIcon.gameObject.SetActive(false);
                    
                    SelectionManager.Instance.HandIsVisible = false;
                }
            }
            else
            {
                SelectionManager.Instance.ontarget = false;
                // Hide the interaction UI if no InteractableObject component is found
                //interaction_Info_UI.SetActive(false);
                SelectionManager.Instance.HandIcon.gameObject.SetActive(false);
                
                SelectionManager.Instance.HandIsVisible = false;
            }
    }
}
