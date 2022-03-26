using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    [SerializeField] int healthPoint;
    [SerializeField] float speed;
    [SerializeField] int NPCLevel;
    [SerializeField] int NPCexpMultiplier;
    RandomItemDropper randomItemDropper;

    private void Start()
    {
        randomItemDropper = GetComponent<RandomItemDropper>();
    }

    public void GetHit(int hit)
    {
        healthPoint -= hit;
        if(healthPoint <= 0)
        {
            randomItemDropper.DrawLoot();
            PlayerStats.Instance.AddExperiencePoint(NPCexpMultiplier * NPCLevel);
            BasicNPCController.Instance.AddToList(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}
