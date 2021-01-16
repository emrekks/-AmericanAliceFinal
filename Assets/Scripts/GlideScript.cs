using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideScript : MonoBehaviour
{

    public bool GlideBool = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            GlideBool = true;
        }
        else
        {
            GlideBool = false;
        }
    }

}
