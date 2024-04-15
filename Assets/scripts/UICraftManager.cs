using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICraftManager : MonoBehaviour
{
    public static UICraftManager Instance{get; set ;}
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
        Transform trackerPrefab = Instantiate(trackerRowPrefab, Vector3.zero, Quaternion.identity);
        trackerPrefab.transform.SetParent(questTrackerContent.transform, false);

        
        int count = trackerRowPrefab.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            trackerRowPrefab.transform.GetChild(i).gameObject.SetActive(false);
            
        }
        
    }
    

    [Header("QuestTracker")]
    public Transform questTrackerContent;
    public Transform trackerRowPrefab;
  
    

    
}
