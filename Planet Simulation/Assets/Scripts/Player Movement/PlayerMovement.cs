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
    public EnergyBar energyBar;

    private Vector3 velocity;
    private bool isGrounded;
    public Skybox skybox;
    void Start()
    {
        energyBar = GameObject.Find("EnergyBar").GetComponent<EnergyBar>();
    }

    void Update()
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
