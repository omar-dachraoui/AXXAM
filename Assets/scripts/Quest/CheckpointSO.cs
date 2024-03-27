using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data", menuName = "ScriptableObjects/CheckpointSO", order = 1)]

public class CheckpointSO : ScriptableObject
{
    public string relatedQuestName ;
    public bool isCompleted = false;
}
