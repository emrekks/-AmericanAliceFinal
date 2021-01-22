using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
   
    public Camera cam;
    public GameObject referance;
    public float Timer = 0f;
    public bool spawn = false;
    public GameObject cat;
    public GameObject textScreen;


    void Start()
    {
        cam = Camera.main;
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
