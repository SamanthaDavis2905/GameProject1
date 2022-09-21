using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float playerSpeed = 6f;
    public Transform groundCheck;
    
    public Vector3 playerVelocity;
    public float gravityValue = -9.81f;
    public float jumpHeight = 1.0f;
    private bool groundedPlayer;
    public LayerMask whatIsGround;

    public float turnSmoothTime = 0.05f;
    float turnSmoothVelocity;

    
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        //checking if you're touching the ground, if so don't move at all vertically
        groundedPlayer = Physics.CheckSphere(groundCheck.position, 0.1f, whatIsGround);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        
        //tracks movement keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

       //if you're moving at all, do crazy math stuff to make you move around and smooth the turns between directional inputs, ie W to WA to A.
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }
        
        //if the player hits the 'jump' button and is grounded, they get thrown upwards
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
           
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
