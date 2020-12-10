using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //DevilForm
    public bool devilOpen = false;
    public float devilFormTimer = 0f;
    
    
    //CharacterController
    public CharacterController controller;

    //Movement
    private float playerSpeed = 2.0f;
    private float jumpHeight = 6f;
    private bool crouch; 

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
    

    private void Start()
    {
        Anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
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
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
        transform.rotation = Quaternion.Euler(0f, Cam.eulerAngles.y, 0f);
       
        //When character movement this code take reference
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }



        //Animations
        Anim.SetFloat("speed.x", vertical);
        Anim.SetFloat("speed.z", horizontal);
        Anim.SetBool("isGrounded", grounded);
        Anim.SetBool("crouch", crouch);


        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
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


        //Grounded Check
        grounded = Physics.CheckSphere(Groundcheck.position, groundRadius, whatIsGround);


        //Jump and Gravity
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            playerGravity.y = jumpHeight;
        }
        else
        {
            playerGravity.y += gravityValue * Time.deltaTime;
        }

        controller.Move(playerGravity * Time.deltaTime);

    }

    //GroundCheck
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
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

    private void DevilForm()
    {
         //DevilForm Upgrades
         if (devilFormTimer >= 15f) 
         {
                devilOpen = false; 
                devilFormTimer = 0f;
         }
         
    }
        
}

