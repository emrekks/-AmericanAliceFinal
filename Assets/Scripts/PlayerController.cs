using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Weapon Controller
    public WeaponController _weaponController;
    
    
    //Player Bigger/Smaller Form
    public bool isSmall = false;
    private Vector3 playerScale;

    //DevilForm
    public bool devilOpen = false;
    public float devilFormTimer = 0f;
    public bool devilFormBigger = false;
    
    
    //CharacterController
    public CharacterController controller;

    //Movement
    public float playerSpeed = 2.0f;
    private float jumpHeight = 6f;
    private bool crouch = false; 

    //MovementMakeSmooth
    private float turnSmoothtime = 0.1f;
    private float turnSmoothVelocity;

    //Gravity
    private float gravityValue = -15.0f;
    
    //GroundCheck
    private Vector3 playerGravity;
    public Transform Groundcheck;
    public float groundRadius = 2.0f;
    public LayerMask whatIsGround;
    private bool grounded;

    //Objects
    public Transform Cam;
    private Animator Anim;

    //Animator Condition
    private bool ForwardRight = false;
    private bool ForwardLeft = false;


    private void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
        playerScale = gameObject.transform.localScale;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeScale();
        }
        
        if (devilOpen == true)
        {
            devilFormTimer += Time.deltaTime;
            DevilForm();
        }

        //Set binds and Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;


        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
        transform.rotation = Quaternion.Euler(0f, Cam.eulerAngles.y, 0f);

        //When character movement this code take reference
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }

        //Animation Conditions



        if (horizontal < -0.01f && vertical > 0.01f)
        {
            ForwardLeft = true;
        }
        else
        {
            ForwardLeft = false;
        }

        int horizontalInt = Convert.ToInt32(horizontal);

        Anim.SetInteger("HorizontalSpeedInteger", horizontalInt);
        Anim.SetFloat("VerticalSpeed", vertical);
        Anim.SetFloat("HorizontalSpeed", horizontal);
        Anim.SetBool("isGrounded", grounded);
        Anim.SetBool("crouch1", crouch);
        Anim.SetBool("ForwardRunningRight", ForwardRight);
        Anim.SetBool("ForwardRunningLeft", ForwardLeft);


        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Anim.SetTrigger("crouch");
            crouch = true;
            controller.center = new Vector3(0, 0.56f, 0);
            controller.height = 0.93f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
            controller.center = new Vector3(0, 0.83f, 0);
            controller.height = 1.6f;
        }

        //Throwing Knife
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Anim.SetTrigger("ThrowKnife");
            //_weaponController.ShootBall();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

        }

        //Grounded Check
        grounded = Physics.CheckSphere(Groundcheck.position, groundRadius, whatIsGround);


        //Jump and Gravity
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Anim.SetTrigger("jumping");
            playerGravity.y = jumpHeight;
        }
        else
        {
            playerGravity.y += gravityValue * Time.deltaTime;
        }

        controller.Move(playerGravity * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Devil"))
        {
            devilOpen = true;
            DevilForm();
            other.gameObject.SetActive(false);
        }
    }

    void ChangeScale()
    {
        if (isSmall == false && devilFormBigger == false)
        {
            gameObject.transform.localScale -=  new Vector3(0.5f,0.5f,0.5f);
            isSmall = true;
        }
        else if (isSmall == true && devilFormBigger == false)
        {
            gameObject.transform.localScale += new Vector3(0.5f,0.5f,0.5f);
            isSmall = false;
        }
    }
    
    

    private void DevilForm()
    {
        if (isSmall == true && devilFormBigger == false)
        {
            devilFormBigger = true;
            gameObject.transform.localScale += new Vector3(1f,1f,1f);
        }
        else if (isSmall == false && devilFormBigger == false)
        {
            devilFormBigger = true;
            gameObject.transform.localScale += new Vector3(0.5f,0.5f,0.5f);
        }

        //DevilForm Upgrades
         if (devilFormTimer >= 15f)
         {
                devilOpen = false;
                devilFormBigger = false;
                devilFormTimer = 0f;
                
                if (isSmall == true && devilFormBigger == false)
                {
                    gameObject.transform.localScale -= new Vector3(1f,1f,1f);
                }
                else if (isSmall == false && devilFormBigger == false)
                {
                    gameObject.transform.localScale -= new Vector3(0.5f,0.5f,0.5f);
                }
         }
    }
        
}

