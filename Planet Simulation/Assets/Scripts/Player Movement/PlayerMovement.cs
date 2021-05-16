using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    // Rover attributes
    public float playerSpeed;
    public float gravity;
    public float jumpHeight;
    public float maxBoostVelocity;
    public float boostRatio;
    private Vector3 velocity;
    private float currentBoost;
    private bool boostLock;

    // Hitbox attributes
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    private bool isGrounded;
    
    // External attributes
    private EnergyBar energyBar;
    private AudioManager sfxManager;

    void Start()
    {
        // External params
        energyBar = gameObject.GetComponent<EnergyBar>();
        sfxManager = GameObject.Find("SFX Manager").GetComponent<AudioManager>();
        // Necessary params
        boostLock = false;
        currentBoost = 1.0f;
    }

    void Update()
    {
        CheckBoost();
        LateralMovement();
    }
    
    // Accelerate the vehicle
    void CheckBoost()
    {
        // Boost if needed
        if (Input.GetButton("Boost"))
        {
            // increase per frame while the boost button is on
            energyBar.DecreaseOnBoost();
            if(currentBoost < maxBoostVelocity)
            {
                currentBoost += boostRatio * Time.deltaTime;
                currentBoost = Mathf.Clamp(currentBoost, 1.0f, maxBoostVelocity);
            }

            if (!boostLock)
            {
                sfxManager.Play("Boost");
                boostLock = true;
            }
        }
        else
        {
            // Have we reached min boost velocity? If not decrease
            if (currentBoost > 1.0f)
            {
                currentBoost -= boostRatio * Time.deltaTime;
                currentBoost = Mathf.Clamp(currentBoost, 1.0f, maxBoostVelocity);
            }
            boostLock = false;
        }
    }

    // Lateral movement in all axis (x y z)
    void LateralMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * playerSpeed * currentBoost * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            sfxManager.Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            energyBar.DecreaseOnJump();
        }
        velocity.y += gravity / 2f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
}
