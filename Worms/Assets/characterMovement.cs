using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float playerSpeed = 6f;
    private Vector3 direction;
    public Transform groundCheck;
    
    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private float jumpHeight = 1.0f;
    private bool groundedPlayer;
    public LayerMask whatIsGround;
    public float playerTotalDistanceTraveled = 0f;
    public float allowedDistancePerTurn = 30f;

    public float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;

    public bool isTurnOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 beginLocationVector3 = transform.position;
        Vector2 beginLocationVector2 = new Vector2(beginLocationVector3.x, beginLocationVector3.z);

        //checking if you're touching the ground, if so don't move at all vertically
        groundedPlayer = Physics.CheckSphere(groundCheck.position, 0.1f, whatIsGround);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //tracks movement keys
        if (!isTurnOver)
        { 
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }
  
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
        if (Input.GetButtonDown("Jump") && groundedPlayer && !isTurnOver)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
           
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        //checks how far the player moved in each frame
        Vector3 endLocationVector3 = transform.position;
        Vector2 endLocationVector2 = new Vector2(endLocationVector3.x, endLocationVector3.z);


        float playerFrameDistanceTraveled = Mathf.Abs((endLocationVector2 - beginLocationVector2).magnitude);
        
        //if you're pressing move, add the distance moved this frame to the total distance traveled
        if(UsingMovementKeys()){
            playerTotalDistanceTraveled += playerFrameDistanceTraveled;
        }
            
        //ends turn and stops movement if you've traveled more distance than is allowed
        if(playerTotalDistanceTraveled >= allowedDistancePerTurn)
        {
            isTurnOver = true;
            direction = Vector3.zero;
        }



        
    
    }

    //checks if player is touching their movement keys (excluding jump)
    private bool UsingMovementKeys()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        if (horizontal != 0 || vertical != 0)
        {
            return true;
        }

        return false;
    }












} //END CLASS


