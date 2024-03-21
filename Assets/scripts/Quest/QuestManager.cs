using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance{get; set ;}
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            
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


    public void  AddActiveQuest(Quest quest)
    {
        allActiveQuests.Add(quest);
        RefreshedQuestList();
    }


    public void MarkCompletedQuest(Quest quest)
    {
        //remove from active
        allActiveQuests.Remove(quest);
        //add to completed
        allCompletedQuests.Add(quest);
        RefreshedQuestList();
    }




    private void Update()
    {
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
            qRow.questName.text = activeQuest.questName;
            qRow.questGiver.text = activeQuest.questGiver;


            qRow.isActive = true;
            qRow.isTracking = false;


            qRow.coinAmount.text = $"{activeQuest.questInfo.coinReward}";

            //qRow.firstReward.sprite = ";
            qRow.firstRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem1Amount}";


            //qRow.secondReward.sprite = ";
            qRow.secondRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem2Amount}";




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

            //qRow.firstReward.sprite = ";
            qRow.firstRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem1Amount}";


            //qRow.secondReward.sprite = ";
            qRow.secondRewardAmount.text = "";//$"{activeQuest.questInfo.rewardItem2Amount}";




        }
    }





}
