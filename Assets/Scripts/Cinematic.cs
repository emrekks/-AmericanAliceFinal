using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cinematic : MonoBehaviour
{
    public PlayerController _playerController;
    public Camera mainCamera;
    public GameObject cutsceneCamera;


    void Start()
    {
        StartCoroutine(startCutscene());
    }
    

    IEnumerator startCutscene()
    {
        mainCamera.enabled = false;
        cutsceneCamera.SetActive(true);
        yield return new WaitForSeconds(6.2f);
        mainCamera.enabled = true;
        cutsceneCamera.SetActive(false);
    }

}

