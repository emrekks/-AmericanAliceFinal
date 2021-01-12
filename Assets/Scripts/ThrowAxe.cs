using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowAxe : MonoBehaviour
{
    public Rigidbody axeRb;


    public float throwForce = 50;


    public Transform target, curve_point;
    public GameObject Hand;



    public Vector3 origLocPos;
    public Vector3 origLocRot;
    private Vector3 pullPosition;

    [Header("Public References")]
    public Transform weapon;
    public AxeController _axeController;

    private float time;
    
    public bool hitedWall = false;
    private bool isReturning = false;



    // Start is called before the first frame update
    void Start()
    {
        origLocPos = weapon.localPosition;
        origLocRot = weapon.localEulerAngles;
        _axeController = GameObject.FindObjectOfType<AxeController>();
    }

    // Update is called once per frame
    void Update()
    {




        if (isReturning)
        {
            if(time < 1f)
            {
                weapon.position = Backtohandwcurve(time, pullPosition, curve_point.position, target.position);
                time += Time.deltaTime * 1.5f;                
            }
            else
            {
                WeaponCatch();
            }
        }
    }

    public void ThrowableAxe()
    {
        _axeController.activated = true;
        axeRb.transform.parent = null;
        axeRb.isKinematic = false;
        axeRb.AddForce(Camera.main.transform.forward * throwForce + transform.up * 2, ForceMode.Impulse);
    }

    public void WeaponStartPull()
    {
        time = 0f;
        _axeController.activated = true;
        pullPosition = weapon.position;
        isReturning = true;
        axeRb.velocity = Vector3.zero;
        axeRb.isKinematic = true;
        hitedWall = false;
        weapon.localEulerAngles = new Vector3(90, 90, 0);
    }

    //Reset Axe
    public void WeaponCatch()
    {
        _axeController.activated = false;
        isReturning = false;
        axeRb.transform.parent = Hand.transform;
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

}
