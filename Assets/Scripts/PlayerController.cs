using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    
    //Rhino Attack
    private RhinoEnemy _rhinoEnemy;
    public Rigidbody _rigidbody;
    
    //Ice Staff
    public GameObject iceStaff;
    private bool isHandlingStaff;
    private bool isAttackingIceStaff;
    public ParticleSystem iceParticles;
    
    //Weapon Controller
    private WeaponController _weaponController;
    private ThrowAxe _throwAxe;
    private PlayerController _playerController;
    private GlideScript _glideScript;
    private WeaponDice _dice;

    //Health
    public int playerHealth = 100;
    private bool playerDead = false;

    //Dice
    public GameObject weaponDice;
    public int DiceCount = 0;
    private bool isDiceHandling = false;


    //Axe
    public bool isHandlingAxe = false;
    public GameObject axeObject;
    private float lastClickedTime;
    private int noClick;
    private float maxComboDelay = 1.5f;
    public bool AttackingColliderEnable = false;

    //Hurt
    private int Hurt;

    //Glide
    private bool Gliding;

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
    public float playerSpeed = 3.0f;
    private float jumpHeight = 6f;
    private bool crouch = false;
    private Vector3 moveDir;
    private int jumpCount = 1;

    //Gravity
    private float gravityValue = -15.0f;
    
    //GroundCheck
    private Vector3 playerGravity;
    public Transform Groundcheck;
    public float groundRadius = 2.0f;
    public LayerMask whatIsGround;
    private bool grounded;

    //Magic Staff
    private float delay = 0f;
    public float shootDelay = 0.1f;
    public bool isAttackingStaff = false;
    public bool isHandlingWand = false;
    public GameObject staff;

    //Objects
    public Transform Cam;
    private Animator Anim;
    public Collider Character, Axe;

    //Animator Condition
    private bool ForwardRight = false;
    private bool ForwardLeft = false;



    //SwitchWeapon
    private bool switchingWeapon;
    private bool isChangingHandToAxe, isChangingHandToStaff, isChangingHandToIceStaff, isChangingHandToDice;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _weaponController = GameObject.FindObjectOfType<WeaponController>();
        _dice = GameObject.FindObjectOfType<WeaponDice>();
        _throwAxe = GameObject.FindObjectOfType<ThrowAxe>();
        _playerController = GetComponent<PlayerController>();
        _glideScript = GameObject.FindObjectOfType<GlideScript>();
        _rhinoEnemy = GameObject.FindWithTag("Rhino").GetComponent<RhinoEnemy>();
        Anim = gameObject.GetComponent<Animator>();
        playerScale = gameObject.transform.localScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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
        Anim.SetBool("Gliding", Gliding);


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

        //Weapon Selection
        if (Input.GetKeyDown(KeyCode.Alpha1) && !axeObject.activeInHierarchy)
        {
            isChangingHandToAxe = true;
            isChangingHandToStaff = false;
            isChangingHandToDice = false;
            isChangingHandToIceStaff = false;

            switchingWeapon = true;
            Anim.SetTrigger("UnarmedToWeapon");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !staff.activeInHierarchy)
        {
            isChangingHandToStaff = true;
            isChangingHandToAxe = false;
            isChangingHandToDice = false;
            isChangingHandToIceStaff = false;

            switchingWeapon = true;
            Anim.SetTrigger("UnarmedToWeapon");
        }
        if (Input.GetKeyDown((KeyCode.Alpha3)) && !iceStaff.activeInHierarchy)
        {
            isChangingHandToAxe = false;
            isChangingHandToStaff = false;
            isChangingHandToDice = false;
            isChangingHandToIceStaff = true;
          
            switchingWeapon = true;
            Anim.SetTrigger("UnarmedToWeapon");

        }

        if (Input.GetKeyDown((KeyCode.Alpha4)) && !weaponDice.activeInHierarchy)
        {
            isChangingHandToAxe = false;
            isChangingHandToStaff = false;
            isChangingHandToDice = true;
            isChangingHandToIceStaff = false;

            switchingWeapon = true;
            Anim.SetTrigger("UnarmedToWeapon");
        }

        //Glide
        if (Input.GetKey(KeyCode.LeftShift) && _glideScript.GlideBool == false)
        {
            playerGravity.y = -2;
            Gliding = true;
        }
       
        if (Input.GetKeyUp(KeyCode.LeftShift) && _glideScript.GlideBool == false || _glideScript.GlideBool == true)
        {
            Gliding = false;
        }

        //Dice
        if (Input.GetMouseButtonDown(0) && isChangingHandToDice == true)
        {
            Anim.SetTrigger("DiceThrowing");
        }


        //Health
        if (playerHealth <= 0)
        {
            playerDead = true;
            playerDeath();
        }
        
        //IceStaff
        if (iceStaff.activeInHierarchy && switchingWeapon == false)
        {
            isHandlingStaff = true;
        }
        else
        {
            isHandlingStaff = false;
        }

        if (isAttackingIceStaff == false && switchingWeapon == false)    //Bug Olabilir Staff Yüzünden
        {
            if (Input.GetMouseButton(0))
            {
                isAttackingIceStaff = true;
                //IceStaffAnimationBurayaGelicek
                iceParticles.Play();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                iceParticles.Stop();
            }

            isAttackingIceStaff = false;
        } 

        //MagicStaff
        if (staff.activeInHierarchy && switchingWeapon == false)
        {
            isHandlingWand = true;
        }
        else 
        {
            isHandlingWand = false;
        }
        
        if (isAttackingStaff == false && isHandlingWand == true)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                isAttackingStaff = true;
                Anim.SetTrigger("Magic");
                delay = shootDelay; 
            }
            isAttackingStaff = false;
        }


        //Throwing Axe
        if (axeObject.activeInHierarchy && switchingWeapon == false)
        {
            isHandlingAxe = true;
        }
        else 
        {
            isHandlingAxe = false;
        }


        //MeleeAttack

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noClick = 0;
        }

        if (Input.GetMouseButtonDown(0) && isHandlingAxe == true && !Input.GetMouseButton(1))
        {
            lastClickedTime = Time.time;
            noClick++;
            if(noClick == 1)
            {
                Anim.SetBool("AttackMelee", true);
                Axe.isTrigger = true;
            }
            noClick = Mathf.Clamp(noClick, 0, 3);
        }


        if (Input.GetMouseButton(1) && Input.GetMouseButtonUp(0) && _throwAxe.hitedWall == false && isHandlingAxe == true)
        {          
            Anim.SetTrigger("Throw");
            Physics.IgnoreCollision(Character, Axe, true);
        }

        if (Input.GetMouseButton(1) && Input.GetMouseButtonUp(0) && _throwAxe.hitedWall == true && isHandlingAxe == true)
        {
            _throwAxe.WeaponStartPull();

        }

        //Grounded Check
        grounded = Physics.CheckSphere(Groundcheck.position, groundRadius, whatIsGround);

        //Jump and Gravity
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            Anim.SetTrigger("jumping");
            playerGravity.y = jumpHeight;
            jumpCount--;
        }

        if (grounded)
        {
            jumpCount = 1;
        }

        else if(Gliding == false)
        {
            playerGravity.y += gravityValue * Time.deltaTime;
        }

        controller.Move(playerGravity * Time.deltaTime);


    }

    public void DiceThrow()
    {
        _dice.ThrowDice();
    }
    public void Combo1()
    {
        if(noClick >= 2)
        {
            Axe.isTrigger = true;
            Anim.SetBool("AttackMelee2", true);
        }
        else
        {
            Axe.isTrigger = false;
            Anim.SetBool("AttackMelee", false);
            noClick = 0;
        }
    }

    public void Combo2()
    {
        if (noClick >= 3)
        {
            Anim.SetBool("AttackMelee3", true);
            Axe.isTrigger = true;
        }
        else
        {
            Anim.SetBool("AttackMelee2", false);
            Anim.SetBool("AttackMelee", false);
            noClick = 0;
            Axe.isTrigger = false;
        }
    }

    public void Combo3()
    {
        Anim.SetBool("AttackMelee", false);
        Anim.SetBool("AttackMelee2", false);
        Anim.SetBool("AttackMelee3", false);
        Axe.isTrigger = false;
        noClick = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Devil"))
        {
            devilOpen = true;
            DevilForm();
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "EnemySword")
        {
            Hurt = UnityEngine.Random.Range(1,3);
            Anim.SetTrigger("Hurt");           
            playerHealth -= 25;
            Anim.SetInteger("DirectionHurt", Hurt);
            
        }
    }

    void playerDeath()
    {
        Anim.SetBool("Death", playerDead);
        _weaponController.enabled = false;
        _throwAxe.enabled = false;
        _playerController.enabled = false;
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
    
    private void Throwing()
    {
        _throwAxe.ThrowableAxe();
    }

    private void unarmedToAxe()
    {
        if(isChangingHandToAxe == true)
        {
            staff.SetActive(false);
            axeObject.SetActive(true);
            weaponDice.SetActive(false);
            iceStaff.SetActive(false);
            switchingWeapon = false;
        }

        if (isChangingHandToStaff == true)
        {
            axeObject.SetActive(false);
            staff.SetActive(true);
            weaponDice.SetActive(false);
            iceStaff.SetActive(false);
            switchingWeapon = false;
        }

        if (isChangingHandToIceStaff == true)
        {
            axeObject.SetActive(false);
            staff.SetActive(false);
            iceStaff.SetActive(true);
            weaponDice.SetActive(false);
            switchingWeapon = false;
        }

        if (isChangingHandToDice == true)
        {
            axeObject.SetActive(false);
            staff.SetActive(false);
            iceStaff.SetActive(false);
            weaponDice.SetActive(true);
            switchingWeapon = false;
        }

    }

    public void AxeColliderEnableTrue()
    {
        AttackingColliderEnable = true;
    }

    public void AxeColliderEnableFalse()
    {
        AttackingColliderEnable = false;
    }

    private void magic()
    {
        _weaponController.ShootBall();
    }

    private void AttackMeleeFalse()
    {
        Anim.SetBool("AttackMelee", false);
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

