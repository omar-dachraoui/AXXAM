using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestRow : MonoBehaviour
{
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questGiver;
    public Button trackingButton;



    public bool isActive;
    public bool isTracking;



    public TextMeshProUGUI coinAmount;

    public Image firstReward;
    public TextMeshProUGUI firstRewardAmount;


    public Image secondReward;
    public TextMeshProUGUI secondRewardAmount;


    public Quest thisQuest;


    private void Start()
    {

        trackingButton.onClick.AddListener(() =>
        {
           if(isActive)
            {
                if (isTracking)
                {
                    isTracking = false;
                    trackingButton.GetComponentInChildren<TextMeshProUGUI>().text = "Track";
                    QuestManager.Instance.RemoveTrackedQuest(thisQuest);
                }
                else
                {
                    isTracking = true;
                    trackingButton.GetComponentInChildren<TextMeshProUGUI>().text = "Untrack";
                    QuestManager.Instance.AddTrackedQuest(thisQuest);
                }
            }
            
        });

    }

}
