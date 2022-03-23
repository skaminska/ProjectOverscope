using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    [SerializeField] int healthPoint;
    [SerializeField] float speed;
    [SerializeField] int NPCLevel;
    [SerializeField] int NPCexpMultiplier;

    public void GetHit(int hit)
    {
        healthPoint -= hit;
        if(healthPoint <= 0)
        {
            GetComponent<Animator>().enabled = false;
            PlayerStats.Instance.AddExperiencePoint(NPCexpMultiplier * NPCLevel);
        }
    }
}
