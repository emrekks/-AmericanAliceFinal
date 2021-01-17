using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDice : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        //Alice = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        rb.AddForce(Camera.main.transform.forward  * 10 * 2, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
