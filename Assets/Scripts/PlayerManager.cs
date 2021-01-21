using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    [SerializeField]private float destroyTimer = 0f;

    public bool heIsDead = false;
    void Update()
    {

        if (EnemyController.Instance.isDead == true)
        {
            heIsDead = true;
        }
        
        
        
        if (heIsDead == true)
        {
            destroyTimer += Time.deltaTime;

            if (destroyTimer <= 10f)
            {
                EnemyController.Instance._enemy.gameObject.SetActive(false);
            }
        }
    }
}
