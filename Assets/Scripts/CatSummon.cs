using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSummon : MonoBehaviour
{

    public Animator catAnim;

    public SpawnObjects spawnObjects; 





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CatFade()
    {
        spawnObjects.cat.SetActive(false);
        spawnObjects.textScreen.SetActive(false);
        spawnObjects.Timer = 0f;
        spawnObjects.spawn = false;
    }
}
