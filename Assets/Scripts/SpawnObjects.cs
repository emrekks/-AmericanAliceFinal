using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    
    public GameObject referance;
    public bool spawn = false;
    public GameObject cat;
    public GameObject textScreen;


    void Start()
    {
        cat = GameObject.FindGameObjectWithTag("Npc");
    }


     void Update()
    {
        
        if (spawn == true)
        {
            textScreen.SetActive(true);
            cat.SetActive(true);
        }
        
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            SpawnIt();
            spawn = true;
        }
        
    }


     void SpawnIt()
     {
         Instantiate(cat, referance.transform.position, Quaternion.identity);
     }
}
