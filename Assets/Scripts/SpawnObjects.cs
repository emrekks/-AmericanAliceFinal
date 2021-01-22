using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjects : MonoBehaviour
{
    
    public GameObject referance;
    public bool spawn = false;
    private float Timer = 0f;
    public GameObject cat;
    public GameObject textScreen;
    public TextMeshProUGUI text;
    public bool firstCalled = false;


    void Start()
    {
        //cat = GameObject.FindGameObjectWithTag("Npc");
    }


     void Update()
    {
        
        if (spawn == true)
        {
            Timer += Time.deltaTime;
            textScreen.SetActive(true);
            cat.SetActive(true);
            
            if (Timer >= 10f)
            {
                cat.SetActive(false);
                textScreen.SetActive(false);
                Timer = 0f;
                spawn = false;
            }
            
        }
        
        
        if(Input.GetKeyDown(KeyCode.C) && spawn == false) 
        {

            cat.transform.position = referance.transform.position;
            
            if (firstCalled == true)
            {
                text.text = "Go to the portal and save us from queen's men.";
                textScreen.SetActive(true);
                spawn = true;
            }
            else if(firstCalled == false)
            {
                textScreen.SetActive(true);
                firstCalled = true;
                spawn = true;
            }
            
            
        }
        
    }


     // void SpawnIt()
     // {
     //     Instantiate(cat, referance.transform.position, Quaternion.identity);
     // }
}
