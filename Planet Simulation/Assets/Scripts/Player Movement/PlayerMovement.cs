using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Constants
    public float maxSpeed = 15f;
    public float minSpeed = 6f;
    public float maxBoostCD = 2f;

    // Boost the player
    public float acceleration = 10f;
    public float deceleration = 10f;
    
    public CharacterController controller;
    
    // Player's stats at the beginning
    public float playerSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    
    // Parameters for ground checks
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Private stats of the player movement
    Vector3 velocity;
    bool isGrounded;
    bool jumped = true;
    bool boosting;

    AudioManager audioManager;

    void Start()
    {
        playerSpeed = minSpeed;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        // Control jump
        HandleJump();
        // Control lateral movement
        HandleLateralMovement();
    }

    void HandleJump()
    {
        // Calculate if it's on the floor
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // If it's on the floor and should is still accelerating or has velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            // Did it previously jump
            if (jumped)
            {
                jumped = false;
                audioManager.Play("Land");

            }
        }

        // If it's on the floor and the jump button was pressed jump w/sound
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            audioManager.Play("Jump");
            jumped = true;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void HandleLateralMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            boosting = true;
            if(playerSpeed < maxSpeed) playerSpeed += acceleration * Time.deltaTime;

            if (!audioManager.IsPlaying("Boost") && boosting)
            {
                audioManager.Play("Boost");
            }
        }
        else
        {
            boosting = false;
            if(playerSpeed > minSpeed)
            {
                playerSpeed -= deceleration * Time.deltaTime;
            }
            else
            {
                playerSpeed = minSpeed;
            }
        }

        controller.Move(move * playerSpeed * Time.deltaTime);
    }
}
