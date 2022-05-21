using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] bool occupied;

    private void Awake()
    {
        occupied = false;
    }

    public bool isOccupied() { return occupied; }

    public void SetOccupiedState(bool state)
    {
        occupied = state;
    }

    public Vector3 GetPatrolPointPosition() { return gameObject.transform.position; } 
}
