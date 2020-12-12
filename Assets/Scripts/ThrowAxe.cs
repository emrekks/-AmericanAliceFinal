using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    public Rigidbody axe;
    public float throwForce = 50;
    public Transform target, curve_point;
    private Vector3 old_pos;
    public GameObject AxeP;
    private bool isReturning = false;
    private float time;
    public GameObject Alice;
    public Collider AxeCol;
    private bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && onGround == false)
        {
            ThrowableAxe();
        }
       
        if (Input.GetKeyUp(KeyCode.E) && onGround == true)
        {
            ReturnAxe();
        }

        if (isReturning)
        {
            if(time < 1f)
            {
                axe.position = Backtohandwcurve(time, old_pos, curve_point.position, target.position);
                axe.rotation = Quaternion.Slerp(axe.transform.rotation, target.rotation, 50 * Time.deltaTime);
                time += Time.deltaTime;                
            }
            else
            {
                ResetAxe();
            }
        }
    }

    void ThrowableAxe()
    {
        axe.transform.parent = null;
        axe.isKinematic = false;
        axe.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        axe.AddTorque(axe.transform.TransformDirection(Vector3.forward) * 100, ForceMode.Impulse);
    }



    void ReturnAxe()
    {
        time = 0f;
        old_pos = AxeP.transform.position;
        isReturning = true;
        axe.velocity = Vector3.zero;
        axe.isKinematic = true;
        onGround = false;
    }

    //Reset Axe
    void ResetAxe()
    {
        isReturning = false;        
        axe.transform.parent = Alice.transform;
        axe.position = target.position;
        axe.rotation = target.rotation;
    }
    Vector3 Backtohandwcurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;       
    }

    void OnCollisionEnter(Collision collision)
    {
        onGround = true;
        axe.isKinematic = true;   
    }


}
