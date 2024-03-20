using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest 
{
    [Header("Bools")]
    public bool Accepted;
    public bool Declined;
    public bool initialDialogCompleted;
    public bool IsCompleted;
    [Header("Quest Info")]
    public QuestInfo questInfo;



}
