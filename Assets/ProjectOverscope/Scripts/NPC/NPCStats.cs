using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    [SerializeField] protected int healthPoint;
    [SerializeField] float speed;
    [SerializeField] int NPCLevel;
    [SerializeField] int NPCexpMultiplier;
    [SerializeField] protected NPCState state;
    RandomItemDropper randomItemDropper;


    private void Start()
    {
        randomItemDropper = GetComponent<RandomItemDropper>();
    }


    public virtual void GetHit(int hit)
    {
        healthPoint -= hit;
        if(healthPoint <= 0)
        {
            randomItemDropper.DrawLoot();
            PlayerStats.Instance.AddExperiencePoint(NPCexpMultiplier * NPCLevel);
            foreach (var quest in PlayerStats.Instance.GetColletedQuests())
            {
                if (quest.GetQuestType() == QuestType.ELIMINATE)
                {
                    if (this.gameObject.tag == ((QuestType_Eliminate)quest.GetQuestRequirements()).GetTargetToEliminateTag())
                    {
                        ((QuestType_Eliminate)quest.GetQuestRequirements()).TargetEliminated();
                        quest.CheckIfQuestCompleted();
                    }
                }
            }
            GetHitAddictionalBehaviour();
            //gameObject.SetActive(false);
        }
    }
    public virtual void GetHitAddictionalBehaviour(){}
}

public enum NPCState {STAND, PATROL, SEE_PLAYER, FIGHT, SEARCH_FOR_PLAYER, GO_BACK_TO_START_POSITION}
