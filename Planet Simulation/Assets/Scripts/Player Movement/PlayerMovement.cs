using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float playerSpeed;
    public float gravity;
    public float jumpHeight;
    public float maxBoostVelocity;
    public float maxBoostTime;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;

    private EnergyBar energyBar;
    private Vector3 velocity;
    private AudioManager sfxManager;
    private float boostVelocity = 1.0f;
    private bool isGrounded;
    private bool boostLock;

    void Start()
    {
        energyBar = gameObject.GetComponent<EnergyBar>();
        sfxManager = GameObject.Find("SFX Manager").GetComponent<AudioManager>();
        boostLock = false;
    }

    void Update()
    {
        CheckBoost();
        LateralMovement();
    }

    void CheckBoost()
    {
        if (Input.GetButton("Boost"))
        {
            boostVelocity = maxBoostVelocity;
            energyBar.DecreaseOnBoost();
            if (!boostLock)
            {
                sfxManager.Play("Boost");
                boostLock = true;
            }
        }
        else
        {
            boostVelocity = 1.0f;
            boostLock = false;
        }
    }

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

        controller.Move(move * playerSpeed * boostVelocity * Time.deltaTime);
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
