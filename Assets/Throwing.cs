using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    public Rigidbody axeRb;
    private float throwPower = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AxeThrow()
    {
        axeRb.isKinematic = false;
        axeRb.transform.parent = null;
        axeRb.AddForce(transform.forward * throwPower, ForceMode.Impulse);
    }
}
