using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float speed = 6f;
    public float gravity = 14f;
    public float verticalVelocity;
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.01f;
    float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move and Camera direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        //Jump    
        verticalVelocity -= -gravity * Time.deltaTime; 
        verticalVelocity = jumpForce;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 jump = new Vector3(0, verticalVelocity, 0);
            controller.Move(jump * Time.deltaTime);
        }

    }
}
