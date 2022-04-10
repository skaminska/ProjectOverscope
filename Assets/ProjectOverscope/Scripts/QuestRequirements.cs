using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRequirements : ScriptableObject
{
    [SerializeField] QuestType questType;
}

public enum QuestType { COLLECT, ELIMINATE, FIND }