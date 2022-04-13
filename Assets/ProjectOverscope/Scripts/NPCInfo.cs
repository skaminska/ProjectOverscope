using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC")]
public class NPCInfo : ScriptableObject
{
    [SerializeField] string NPCname;

    [SerializeField] QuestGiver questGiver;

    public void SetQuestGiver(QuestGiver questGiver)
    {
        this.questGiver = questGiver;
    }

    public QuestGiver GetQuestGiver()
    {
        return questGiver;
    }
}
