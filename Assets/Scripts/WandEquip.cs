using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandEquip : MonoBehaviour
{

    public GameObject wand;
    
    
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
            wand.gameObject.SetActive(false);
        }
    }
}
