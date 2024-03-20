using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public bool playerinrange;
    public bool isTalkingWithPlayer;
    
    TextMeshProUGUI NPCDialogtext;
    Button optionButton1;
    TextMeshProUGUI optionButtonText1;
    Button optionButton2;
    TextMeshProUGUI optionButtonText2;
    public List<Quest> questList;
    public Quest currentActiveQuest = null  ;
    public int activeQuestIndex = 0 ;
    public bool firstTimeInteraction = true ;
    public int currentDialog ;


    // Start is called before the first frame update
    void Start()
    {
        // Get the text component of the NPC
        NPCDialogtext =  DialogueSystem.Instance.Dialogetxt;
        optionButton1 = DialogueSystem.Instance.Option1BTN;
        optionButtonText1 = DialogueSystem.Instance.Option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        optionButton2 = DialogueSystem.Instance.Option2BTN;
        optionButtonText2 = DialogueSystem.Instance.Option2BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
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
    public void StartConversation()
    {
        isTalkingWithPlayer = true;
        if(firstTimeInteraction)
        {
            firstTimeInteraction = false ;
            currentActiveQuest = questList[activeQuestIndex]; // 0 at the start 
            StartQuestInitialDialog();
            currentDialog = 0 ;
        }
        else
        {

        }
        
         

        
    }
    public void StartQuestInitialDialog()
    {
        DialogueSystem.Instance.OpenDialogUI();
        NPCDialogtext.text = currentActiveQuest.questInfo.initialDialog[currentDialog];
        optionButtonText1.text = "Next" ;
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(()=>{
            currentDialog++;
            CheckIfDialogDone();
        });
        optionButton2.gameObject.SetActive(false);

    }
    public void CheckIfDialogDone()
    {

    }
}
