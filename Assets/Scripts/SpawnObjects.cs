using System;
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
    public bool level1 = true;
    public bool level2 = false;
    public bool npcLittle = false;
    public LevelUp levelUp;
    public AudioSource welcome;
    public AudioSource portal;
    public AudioSource dungeon;
    public AudioSource shrink;
    public AudioSource tired;


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


        if (Input.GetKeyDown(KeyCode.C) && spawn == false)
        {

            cat.transform.position = referance.transform.position;

            if (firstCalled == true && level1 == true && npcLittle == false && level2 == false)
            {
                text.text = "Go to the portal and save us from queen's men.";
                textScreen.SetActive(true);
                spawn = true;
                portal.Play();
            }
            else if (firstCalled == false && level1 == true && npcLittle == false)
            {
                textScreen.SetActive(true);
                firstCalled = true;
                spawn = true;
                welcome.Play();
            }

            if (firstCalled == true && level2 == true && npcLittle == false)
            {
                text.text = "We are tired of both the queen and her men.";
                textScreen.SetActive(true);
                spawn = true;
                tired.Play();
            }
            else if (firstCalled == false && level2 == true && npcLittle == false)
            {
                textScreen.SetActive(true);
                firstCalled = true;
                spawn = true;
                dungeon.Play();
                
            }

            if (npcLittle == true)
            {
                text.text = "What are you waiting for to shrink?";
                textScreen.SetActive(true);
                spawn = true;
                shrink.Play();
            }

        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NpcLittle"))
        {
            npcLittle = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NpcLittle"))
        {
            npcLittle = false;
        }



        // void SpawnIt()
        // {
        //     Instantiate(cat, referance.transform.position, Quaternion.identity);
        // }
    }
}
