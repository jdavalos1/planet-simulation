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

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    private EnergyBar energyBar;
    private Vector3 velocity;
    private bool isGrounded;
    private AudioManager sfxManager;

    void Start()
    {
        energyBar = gameObject.GetComponent<EnergyBar>();
        sfxManager = GameObject.Find("SFX Manager").GetComponent<AudioManager>();
    }

    void Update()
    {
        LateralMovement();
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

        controller.Move(move * playerSpeed * Time.deltaTime);
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
