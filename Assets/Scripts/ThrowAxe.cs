using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    public Rigidbody axeRb;

    public float throwForce = 50;

    public Transform target, curve_point;
    public GameObject AxeP;
    public GameObject Alice;
    public Collider AxeCol;

    private Vector3 origLocPos;
    private Vector3 origLocRot;
    private Vector3 pullPosition;

    [Header("Public References")]
    public Transform weapon;
    public Transform hand;
    public Transform curvePoint;

    private float time;
    
    private bool hitedWall = false;
    private bool isReturning = false;

    // Start is called before the first frame update
    void Start()
    {
        origLocPos = weapon.localPosition;
        origLocRot = weapon.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && hitedWall == false)
        {
            ThrowableAxe();
        }
       
        if (Input.GetKeyUp(KeyCode.E) && hitedWall == true)
        {
            WeaponStartPull();
        }

        if (isReturning)
        {
            if(time < 1f)
            {
                weapon.position = Backtohandwcurve(time, pullPosition, curve_point.position, target.position);
                //axeRb.rotation = Quaternion.Slerp(axeRb.transform.rotation, target.rotation, 50 * Time.deltaTime);
                time += Time.deltaTime * 1.5f;                
            }
            else
            {
                WeaponCatch();
            }
        }
    }

    void ThrowableAxe()
    {
        axeRb.transform.parent = null;
        axeRb.isKinematic = false;
        axeRb.AddTorque(axeRb.transform.TransformDirection(Vector3.forward) * 100, ForceMode.VelocityChange);
        axeRb.AddForce(Camera.main.transform.forward * throwForce + transform.up * 2, ForceMode.Impulse);
        //axeRb.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);

    }



    void WeaponStartPull()
    {
        time = 0f;
        pullPosition = weapon.position;
        isReturning = true;
        axeRb.velocity = Vector3.zero;
        axeRb.isKinematic = true;
        hitedWall = false;
    }

    //Reset Axe
    void WeaponCatch()
    {
        isReturning = false;
        axeRb.transform.parent = Alice.transform;
        weapon.localEulerAngles = origLocRot;
        weapon.localPosition = origLocPos;
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
        hitedWall = true;
        axeRb.isKinematic = true;
        //if (gameObject.name == "AliceChar")
        //{
        //    AxeCol.isTrigger = true;
        //}
        //else
        //{
        //    AxeCol.isTrigger = false;
        //}
    }


}
