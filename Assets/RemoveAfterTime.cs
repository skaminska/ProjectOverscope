using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterTime : MonoBehaviour
{
    [SerializeField] float time;
    
    void Start()
    {
        Invoke("RemoveComponent", time);
    }

    void RemoveComponent()
    {
        Destroy(gameObject);
    }

}
