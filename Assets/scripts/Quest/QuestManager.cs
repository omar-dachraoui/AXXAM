using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance{get; set ;}
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
        }
        
        
    }
    
    public List<Quest> allActiveQuests ;
    public List<Quest> allCompletedQuests ;
    [Header("QuestMenu")]
    public GameObject questMenu;
    public bool isQuestMenuOpen;
    public GameObject activeQuestPrefab;
    public GameObject completedQuestPrefab;
    public GameObject questMenuContent;
    [Header("QuestTracker")]
    public GameObject questTrackerContent;
    public GameObject trackerRowPrefab;
    public List<Quest> allTrackedQuests;


    [SerializeField] private InteractableObject interactableObject  ;
    [SerializeField] private CheckpointSO checkpointSO;


    private void Start()
    {
        if(checkpointSO != null)
        {
            checkpointSO.isCompleted = false;
        }
        
    }

    private void InteractableObject_OnRuneDetected(object sender, EventArgs e)
    {
        checkpointSO.isCompleted = true;
    }

    public void AddTrackedQuest(Quest quest)
    {
        allTrackedQuests.Add(quest);
        RefreshedTrackedQuestList();
    }
    public void RemoveTrackedQuest(Quest quest)
    {
        allTrackedQuests.Remove(quest);
        RefreshedTrackedQuestList();
    }

   

    public void  AddActiveQuest(Quest quest)
    {
        allActiveQuests.Add(quest);
        //add to tracked
        AddTrackedQuest(quest);

        RefreshedQuestList();
    }


    public void MarkCompletedQuest(Quest quest)
    {
        //remove from active
        allActiveQuests.Remove(quest);
        //add to completed
        allCompletedQuests.Add(quest);
        //remove from tracked
        RemoveTrackedQuest(quest);
        RefreshedQuestList();
    }




    private void Update()
    {
        RefreshedTrackedQuestList();
        // Check for input to toggle the inventory screen
        if (Input.GetKeyDown(KeyCode.Q) && !isQuestMenuOpen)
        {
            // Open the inventory screen
            questMenu.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.Instance.disableselction();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            isQuestMenuOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isQuestMenuOpen)
        {
            // Close the inventory screen
            questMenu.SetActive(false);
            if (!CraftingSystem.Instance.isOpen || !InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                SelectionManager.Instance.enableselction();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
                Cursor.visible = false;
            }
            isQuestMenuOpen = false;
            
        }
    }
    public void RefreshedTrackedQuestList()
    {
        // Destroying the previous list
        foreach (Transform child in questTrackerContent.transform)
        {
            Destroy(child.gameObject);
        }
 
        foreach (Quest trackedQuest in allTrackedQuests)
        {
            GameObject trackerPrefab = Instantiate(trackerRowPrefab, Vector3.zero, Quaternion.identity);
            trackerPrefab.transform.SetParent(questTrackerContent.transform, false);
 
            TrackerRow tRow = trackerPrefab.GetComponent<TrackerRow>();
 
            tRow.questName.text = trackedQuest.questName;
            tRow.questDescription.text = trackedQuest.Description;


            var req1 = trackedQuest.questInfo.firstRequirmentItem ;
            var req1Amount = trackedQuest.questInfo.firstRequirementAmount;
            var req2 = trackedQuest.questInfo.secondRequirmentItem ;
            var req2Amount = trackedQuest.questInfo.secondRequirementAmount;
 
            if (req2!= "") // if we have 2 requirements
            {
                tRow.questRequirement.text = $"{req1} " + InventorySystem.Instance.CountItem(req1)+"/" + $"{req1Amount}\n" +
               $"{req2 } " + InventorySystem.Instance.CountItem(req2 )+"/" + $"{req2Amount}\n";
            }
            else // if we have only one
            {
                tRow.questRequirement.text = $"{req1} " + InventorySystem.Instance.CountItem(req1)+"/" + $"{req1Amount}\n";
            }


            if(trackedQuest.questInfo.hasCheckpoints)
            {
                var existingText = tRow.questRequirement.text;
                tRow.questRequirement.text = PrintCheckpoints(trackedQuest,existingText);
                interactableObject.OnRuneDetected += InteractableObject_OnRuneDetected;
               

            }
 
 
        }
 
    }

    private string PrintCheckpoints(Quest trackedQuest, string existingText)
    {
        string finalText = existingText;
        foreach (CheckpointSO cp in trackedQuest.questInfo.checkpoints)
        {
            if (cp.isCompleted)
            {
                finalText += $"<color=green>{cp.relatedQuestName} is completed</color>\n";
            }
            else
            {
                finalText += $"<color=red>{cp.relatedQuestName} is not completed</color>\n";
            }
        }
        return finalText;
    }

    public void RefreshedQuestList()
    {
        foreach (Transform child in questMenuContent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Quest activeQuest in allActiveQuests)
        {
            GameObject questPrefab = Instantiate(activeQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(questMenuContent.transform,false);
            QuestRow qRow = questPrefab.GetComponent<QuestRow>();

            qRow.thisQuest = activeQuest;


            qRow.questName.text = activeQuest.questName;
            qRow.questGiver.text = activeQuest.questGiver;


            qRow.isActive = true;
            qRow.isTracking = true;


            qRow.coinAmount.text = $"{activeQuest.questInfo.coinReward}";

            if(activeQuest.questInfo.rewardItem1 != "")
           {
            qRow.firstReward.sprite = GetSpriteForItem(activeQuest.questInfo.rewardItem1);
            qRow.firstRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem1Amount}";
           }
            else
            {
                qRow.firstReward.gameObject.SetActive(false) ;
                qRow.firstRewardAmount.text = "";
            }


           if(activeQuest.questInfo.rewardItem2 != "")
           {
            qRow.secondReward.sprite = GetSpriteForItem(  activeQuest.questInfo.rewardItem2);
            qRow.secondRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem2Amount}";
           }
            else
            {
                qRow.secondReward.gameObject.SetActive(false) ;
                qRow.secondRewardAmount.text = "";
            }




        }
         foreach (Quest completedQuest in allCompletedQuests)
        {
            GameObject questPrefab = Instantiate(completedQuestPrefab, Vector3.zero, Quaternion.identity);
            questPrefab.transform.SetParent(questMenuContent.transform,false);
            QuestRow qRow = questPrefab.GetComponent<QuestRow>();
            qRow.questName.text = completedQuest.questName;
            qRow.questGiver.text = completedQuest.questGiver;


            qRow.isActive = false;
            qRow.isTracking = false;


            qRow.coinAmount.text = $"{completedQuest.questInfo.coinReward}";

           if(completedQuest.questInfo.rewardItem1 != "")
           {
            qRow.firstReward.sprite = GetSpriteForItem(completedQuest.questInfo.rewardItem1);
            qRow.firstRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem1Amount}";
           }
            else
            {
                qRow.firstReward.gameObject.SetActive(false) ;
                qRow.firstRewardAmount.text = "";
            }


           if(completedQuest.questInfo.rewardItem2 != "")
           {
            qRow.secondReward.sprite = GetSpriteForItem(completedQuest.questInfo.rewardItem2);
            qRow.secondRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem2Amount}";
           }
            else
            {
                qRow.secondReward.gameObject.SetActive(false) ;
                qRow.secondRewardAmount.text = "";
            }


            

            


           




        }
    }

    private Sprite GetSpriteForItem(string item)
    {
        var itemToGet= Resources.Load<GameObject>( item + "_UI");
        
    
        return itemToGet.GetComponent<Image>().sprite;
        
    }
}
