using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNPCController : Singleton<BasicNPCController>
{
    [SerializeField] List<Transform> locations;
    [SerializeField] GameObject npcPrefab;
    [SerializeField] List<GameObject> npcList;

    private void Start()
    {
        for(int i =0; i<10; i++)
            npcList.Add(Instantiate(npcPrefab));
        foreach (var npc in npcList)
        {
            npc.SetActive(false);
        }
    }

    private void Update()
    {
        if (npcList.Count > 0)
        {
            GameObject npc = npcList[0];
            npc.transform.position = locations[Random.Range(0, locations.Count)].position;
            npc.SetActive(true);
            npc.GetComponent<NPCMovement>().SetDestination(locations[Random.Range(0, locations.Count)]);
            npcList.Remove(npc);
            
        }

    }
    public void AddToList(GameObject npc)
    {
        npcList.Add(npc);
    }
}
