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
}
