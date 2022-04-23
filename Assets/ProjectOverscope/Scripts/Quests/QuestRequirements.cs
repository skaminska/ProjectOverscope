using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRequirements : ScriptableObject
{
    [SerializeField] QuestType questType;
    [SerializeField] public bool completed;

    public QuestType GetQuestType()
    {
        return questType;
    }

    public virtual void QuestSetUp() { }

    public virtual string GetQuestProgress() { return ""; }

    //TODO some general function to change quest requerements values
}

public enum QuestType { COLLECT, ELIMINATE, FIND, TALKTO }