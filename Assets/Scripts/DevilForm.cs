using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilForm : MonoBehaviour
{

    public PlayerController playerController;
    
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
            playerController.devilOpen = true;
            gameObject.SetActive(false);
        }
    }
}
