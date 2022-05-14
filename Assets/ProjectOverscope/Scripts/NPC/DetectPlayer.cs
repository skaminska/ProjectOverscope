using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    NPCAgressive NPCAgressive;

    [SerializeField] 

    private void Start()
    {
        NPCAgressive = GetComponentInParent<NPCAgressive>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            NPCAgressive.PlayerSeen(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            NPCAgressive.PlayerLost();
        }
    }
}
