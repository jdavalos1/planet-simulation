using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera[] roverCameras;
    public Camera thirdPerson;

    private ThirdPersonMouseFollow thirdPersonScript;
    private MouseFollow cameraMouseFollowScript;
    private PlayerMovement playerMovementScript;
    
    private void Start()
    {
        thirdPersonScript = FindObjectOfType<ThirdPersonMouseFollow>();
        cameraMouseFollowScript = FindObjectOfType<MouseFollow>();
        playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        MainCameraViewSwap();
    }
    
    void MainCameraViewSwap()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (thirdPerson.enabled)
            {
                thirdPerson.enabled = false;
                thirdPersonScript.enabled = false;
                playerMovementScript.enabled = true;
                cameraMouseFollowScript.enabled = true;
                foreach (var camera in roverCameras) camera.enabled = true;
            }
            else
            {
                thirdPerson.enabled = true;
                thirdPersonScript.enabled = true;
                playerMovementScript.enabled = false;
                cameraMouseFollowScript.enabled = false;
                foreach (var camera in roverCameras) camera.enabled = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwapCurrentCameras(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapCurrentCameras(2);
        }

    }
    void SwapCurrentCameras(int i)
    {
        var tempTexture = roverCameras[i].targetTexture;
        roverCameras[i].targetTexture = roverCameras[0].targetTexture;
        roverCameras[0].targetTexture = tempTexture;

        var temp = roverCameras[i];
        roverCameras[i] = roverCameras[0];
        roverCameras[0] = temp;
    }
}
