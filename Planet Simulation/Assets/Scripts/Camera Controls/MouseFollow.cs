using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    // Angle from the center of the screen
    [Range(0, 90f)]
    public float maxSpotlightAngle = 90f;

    // Information for the movable objects
    public Transform player;
    public Light[] spotlights;

    // Spotlight Rotation controls
    private float[] leftRotations;
    private float[] rightRotations;

    // Rotation of the camera
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        leftRotations = new float[2] { 0f, 0f };
        rightRotations = new float[2] { 0f, 0f };
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftControl) && spotlights[0].enabled)
        {
            // Handle left rotation movement
            leftRotations[0] -= mouseY;
            leftRotations[1] += mouseX;

            leftRotations[0] = Mathf.Clamp(leftRotations[0], -maxSpotlightAngle, maxSpotlightAngle);
            leftRotations[1] = Mathf.Clamp(leftRotations[1], -maxSpotlightAngle, maxSpotlightAngle);

            spotlights[0].transform.localRotation = Quaternion.Euler(leftRotations[0], leftRotations[1], 0f);
        }
        else if (Input.GetKey(KeyCode.LeftAlt) && spotlights[1].enabled)
        {
            // Handle right spotlight movement
            rightRotations[0] -= mouseY;
            rightRotations[1] += mouseX;

            rightRotations[0] = Mathf.Clamp(rightRotations[0], -maxSpotlightAngle, maxSpotlightAngle);
            rightRotations[1] = Mathf.Clamp(rightRotations[1], -maxSpotlightAngle, maxSpotlightAngle);

            spotlights[1].transform.localRotation = Quaternion.Euler(rightRotations[0], rightRotations[1], 0f);
        }
        else
        {
            // Handle camera movement
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -maxSpotlightAngle, maxSpotlightAngle);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            player.Rotate(Vector3.up * mouseX);
        }
    }
}
