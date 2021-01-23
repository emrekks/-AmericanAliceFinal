using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject player;

    private Vector3 referancePlayer;
    public bool inPortaled = false;

    public GameObject bluePot;
    public GameObject redPot;
    public GameObject light1;
    public GameObject light2;
    public GameObject bossEnemy;
    public GameObject portal2;
    
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player"); 
       referancePlayer = GameObject.FindGameObjectWithTag("PortalReferance").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPortaled == true)
        {
            bluePot.SetActive(true);
            redPot.SetActive(true);
            bossEnemy.SetActive(true);
            portal2.SetActive(true);
            light1.SetActive(true);
            light2.SetActive(true);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Transport");
            player.transform.position = referancePlayer;
            inPortaled = true;
        }
    }
}
