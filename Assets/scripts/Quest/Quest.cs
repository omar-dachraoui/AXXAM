using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest 
{
    public string questName;
    public string questGiver;
    public string Description;

    [Header("Bools")]
    public bool Accepted;
    public bool Declined;
    public bool initialDialogCompleted;
    public bool IsCompleted;
    public bool hasNoRequirements;
    [Header("Quest Info")]
    public QuestInfo questInfo;



}
