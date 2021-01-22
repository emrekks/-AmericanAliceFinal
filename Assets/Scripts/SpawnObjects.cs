using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
   
    public Camera cam;
    public GameObject referance;
    public float Timer = 0f;
    public GameObject objToSpawn;
    public bool spawn = false;
    private GameObject cube;


     void Start()
    {
        cam = Camera.main;
        
    }


     void Update()
    {
        if (spawn == true)
        {
            Timer += Time.deltaTime;  
            if(Timer >= 5f)
            {
                Destroy(cube);
                Timer = 0f;
                spawn = false;
            }
        }
        
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            SpawnIt();
            spawn = true;

        }
       
        
    }


     void SpawnIt()
    {
        Instantiate(objToSpawn,referance.transform.position,Quaternion.identity);
        cube=GameObject.FindGameObjectWithTag("Npc");
    }
}
