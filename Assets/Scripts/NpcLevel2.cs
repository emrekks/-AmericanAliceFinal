using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLevel2 : MonoBehaviour
{
    public SpawnObjects _spawnObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _spawnObjects.level2 = true;
        }
    }
}
