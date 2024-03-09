using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public bool playerinrange;
    public bool isTalkingWithPlayer;
    

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
    public void StartConversation()
    {
        isTalkingWithPlayer = true;
        DialogueSystem.Instance.OpenDialogUI();
        DialogueSystem.Instance.Dialogetxt.text = "Hello there!";
        DialogueSystem.Instance.Option1BTN.transform.Find("Text (TMP)").GetComponent<TMPro.TextMeshProUGUI>().text = "BYE";

        DialogueSystem.Instance.Option1BTN.onClick.AddListener(() => {DialogueSystem.Instance.CloseDialogUI(); isTalkingWithPlayer = false; });

         

        
    }
}
