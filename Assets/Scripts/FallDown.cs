using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    public float targetTime = 3f;
    public AudioSource fall;
    public bool down=false;
   
    // Update is called once per frame
    
    
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fall.Play();
        }
    }
    
}
