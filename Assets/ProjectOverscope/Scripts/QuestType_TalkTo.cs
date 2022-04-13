using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestTypes/TalkTo")]
public class QuestType_TalkTo : QuestRequirements
{
    [SerializeField] List<NPCInfo> NPCs;

    public void Talked(NPCInfo npc)
    {
        NPCs.Remove(npc);
        if(NPCs.Count == 0)
        {
            base.completed = true;
        }
    }

    public override void QuestSetUp()
    {
        foreach(var npc in NPCs)
        {
            npc.GetQuestGiver().TurnOnInteractions();
        }
    }
}
