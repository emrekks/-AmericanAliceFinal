using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;
    
    private void Awake()
    {
        instance = this;
    }

    #endregion


    public string levelName;

    public GameObject pauseMenu;
    private bool pauseOpen = false;
    

    void Start()
    {
       // pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (pauseOpen == false)
            {
                pauseMenu.SetActive(true);
                pauseOpen = true;
                Time.timeScale = 0f;
            }
            else if(pauseOpen == true)
            {
                pauseMenu.SetActive(false);
                pauseOpen = false;
                Time.timeScale = 1f;
            }
        }

    }
    

    public void LoadScene()
    {
        SceneManager.LoadScene(levelName);
    }

}
