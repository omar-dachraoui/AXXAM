using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public bool playerInRange;
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


     private void Start()
    {
        NPCDialogtext = DialogueSystem.Instance.Dialogetxt;
 
        optionButton1 = DialogueSystem.Instance.Option1BTN;
        optionButtonText1 = DialogueSystem.Instance.Option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
 
        optionButton2 = DialogueSystem.Instance.Option2BTN;
        optionButtonText2 = DialogueSystem.Instance.Option2BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
 
    }
 
 
    public void StartConversation()
    {
        isTalkingWithPlayer = true;
 
       // LookAtPlayer();
 
        // Interacting with the NPC for the first time
        if (firstTimeInteraction)
        {
            firstTimeInteraction = false;
            currentActiveQuest = questList[activeQuestIndex]; // 0 at start
            StartQuestInitialDialog();
            currentDialog = 0;
        }
        else // Interacting with the NPC after the first time
        {
 
            // If we return after declining the quest
            if (currentActiveQuest.Declined)
            {
 
                DialogueSystem.Instance.OpenDialogUI();
 
                NPCDialogtext.text = currentActiveQuest.questInfo.comebackAfterDecline;
 
                SetAcceptAndDeclineOptions();
            }
 
 
            // If we return while the quest is still in progress
            if (currentActiveQuest.Accepted && currentActiveQuest.IsCompleted == false)
            {
                if (AreQuestRequirmentsCompleted())
                {
 
                    SubmitRequiredItems();
 
                    DialogueSystem.Instance.OpenDialogUI();
 
                    NPCDialogtext.text = currentActiveQuest.questInfo.comebackCompleted;
 
                    optionButtonText1.text = "[Take Reward]";
                    optionButton1.onClick.RemoveAllListeners();
                    optionButton1.onClick.AddListener(() => {
                        ReceiveRewardAndCompleteQuest();
                    });
                }
                else
                {
                    DialogueSystem.Instance.OpenDialogUI();
 
                    NPCDialogtext.text = currentActiveQuest.questInfo.comebackInProgress;
 
                    optionButtonText1.text = "[Close]";
                    optionButton1.onClick.RemoveAllListeners();
                    optionButton1.onClick.AddListener(() => {
                        DialogueSystem.Instance.CloseDialogUI();
                        isTalkingWithPlayer = false;
                    });
                }
            }
 
            if (currentActiveQuest.IsCompleted == true)
            {
                DialogueSystem.Instance.OpenDialogUI();
 
                NPCDialogtext.text = currentActiveQuest.questInfo.finalWords;
 
                optionButtonText1.text = "[Close]";
                optionButton1.onClick.RemoveAllListeners();
                optionButton1.onClick.AddListener(() => {
                DialogueSystem.Instance.CloseDialogUI();
                isTalkingWithPlayer = false;


                });
            }
 
            // If there is another quest available
            if (currentActiveQuest.initialDialogCompleted == false)
            {
                StartQuestInitialDialog();
            }
 
        }
 
    }
 
    private void SetAcceptAndDeclineOptions()
    {
        optionButtonText1.text = currentActiveQuest.questInfo.acceptOption;
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(() => {
        AcceptedQuest();
        });
 
        optionButton2.gameObject.SetActive(true);
        optionButtonText2.text = currentActiveQuest.questInfo.declineOption;
        optionButton2.onClick.RemoveAllListeners();
        optionButton2.onClick.AddListener(() => {
        DeclinedQuest();
        });
    }
 
    private void SubmitRequiredItems()
    {
        string firstRequiredItem = currentActiveQuest.questInfo.firstRequirmentItem;
        int firstRequiredAmount = currentActiveQuest.questInfo.firstRequirementAmount;
 
        if (firstRequiredItem != "")
        {
            InventorySystem.Instance.RemoveItem(firstRequiredItem, firstRequiredAmount);
        }
 
 
        string secondtRequiredItem = currentActiveQuest.questInfo.secondRequirmentItem;
        int secondRequiredAmount = currentActiveQuest.questInfo.secondRequirementAmount;
 
        if (firstRequiredItem != "")
        {
            InventorySystem.Instance.RemoveItem(secondtRequiredItem, secondRequiredAmount);
        }
 
    }
 
    private bool AreQuestRequirmentsCompleted()
    {
        print("Checking Requirments");
 
        // First Item Requirment
 
        string firstRequiredItem = currentActiveQuest.questInfo.firstRequirmentItem;
        int firstRequiredAmount = currentActiveQuest.questInfo.firstRequirementAmount;
 
        var firstItemCounter = 0;
 
        foreach (string item in InventorySystem.Instance.itemList)
        {
            if (item == firstRequiredItem)
            {
                firstItemCounter++;
            }
        }
 
        // Second Item Requirment -- If we dont have a second item, just set it to 0
 
        string secondRequiredItem = currentActiveQuest.questInfo.secondRequirmentItem;
        int secondRequiredAmount = currentActiveQuest.questInfo.secondRequirementAmount;
 
        var secondItemCounter = 0;
 
        foreach (string item in InventorySystem.Instance.itemList)
        {
            if (item == secondRequiredItem)
            {
                secondItemCounter++;
            }
        }
 
        if (firstItemCounter >= firstRequiredAmount && secondItemCounter >= secondRequiredAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 
    private void StartQuestInitialDialog()
    {
        DialogueSystem.Instance.OpenDialogUI();
 
        NPCDialogtext.text = currentActiveQuest.questInfo.initialDialog[currentDialog];
        optionButtonText1.text = "Next";
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(()=> {
        currentDialog++;
        CheckIfDialogDone();
        });
 
        optionButton2.gameObject.SetActive(false);
    }
 
    private void CheckIfDialogDone()
    {
        if (currentDialog == currentActiveQuest.questInfo.initialDialog.Count - 1) // If its the last dialog 
        {
            NPCDialogtext.text = currentActiveQuest.questInfo.initialDialog[currentDialog];
 
            currentActiveQuest.initialDialogCompleted = true;
 
            SetAcceptAndDeclineOptions();
        }
        else  // If there are more dialogs
        {
            NPCDialogtext.text = currentActiveQuest.questInfo.initialDialog[currentDialog];
 
            optionButtonText2.text = "Next";
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => {
            currentDialog++;
            CheckIfDialogDone();
            });
        }
    }
    private void AcceptedQuest()
    {
        QuestManager.Instance.AddActiveQuest(currentActiveQuest);
        currentActiveQuest.Accepted = true;
        currentActiveQuest.Declined = false;
 
        if (currentActiveQuest.hasNoRequirements)
        {
            NPCDialogtext.text = currentActiveQuest.questInfo.comebackCompleted;
            optionButtonText1.text = "[Take Reward]";
            optionButton1.onClick.RemoveAllListeners();
            optionButton1.onClick.AddListener(() => {
            ReceiveRewardAndCompleteQuest();
            });
            optionButton2.gameObject.SetActive(false);
        }
        else
        {
            NPCDialogtext.text = currentActiveQuest.questInfo.acceptAnswer;
            CloseDialogUI();
        }
 
 
 
    }
 
    private void CloseDialogUI()
    {
        optionButtonText1.text = "[Close]";
        optionButton1.onClick.RemoveAllListeners();
        optionButton1.onClick.AddListener(() => {
        DialogueSystem.Instance.CloseDialogUI();
        isTalkingWithPlayer = false;
        });
        optionButton2.gameObject.SetActive(false);
    }
 
    private void ReceiveRewardAndCompleteQuest()
    {
        QuestManager.Instance.MarkCompletedQuest(currentActiveQuest);
        
        currentActiveQuest.IsCompleted = true;
 
        var coinsRecieved = currentActiveQuest.questInfo.coinReward;
        print("You recieved " + coinsRecieved + " gold coins");
 
        if (currentActiveQuest.questInfo.rewardItem1 != "")
        {
            InventorySystem.Instance.AddToInventory(currentActiveQuest.questInfo.rewardItem1);
        }
 
        if (currentActiveQuest.questInfo.rewardItem2 != "")
        {
            InventorySystem.Instance.AddToInventory(currentActiveQuest.questInfo.rewardItem2);
        }
 
        activeQuestIndex++;
 
        // Start Next Quest 
        if (activeQuestIndex < questList.Count)
        {
            currentActiveQuest = questList[activeQuestIndex];
            currentDialog = 0;
            DialogueSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        }
        else
        {
            DialogueSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
            print("No more quests");
        }
 
    }
 
    private void DeclinedQuest()
    {
        currentActiveQuest.Declined = true;
 
        NPCDialogtext.text = currentActiveQuest.questInfo.declineAnswer;
        CloseDialogUI();
    }
 
  
 
    /*public void LookAtPlayer()
    {
        var player = PlayerState.Instance.playerBody.transform;
        Vector3 direction = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
 
        var yRotation = transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0,yRotation,0);
 
    }*/
 
 
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}