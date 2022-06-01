using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] List<NPCAgressive> enemies;
    [SerializeField] List<PatrolPoint> patrolPoints;
    [SerializeField] List<Objective> locationObiectives;


    public PatrolPoint GetUnoccupiedPatrolPosition()
    {
        PatrolPoint point;
        do
        {
            point = patrolPoints[Random.Range(0, patrolPoints.Count)];
        }
        while (point.isOccupied() != false);
        return point;
    }

    public void CheckIfLocationComplete()
    {
        if (enemies.Find((x) => x.isActiveAndEnabled == true))
        {
            Debug.Log("All enemied die");
            PlayerStats.Instance.AddExperiencePoint(5);
        }
    }
}
