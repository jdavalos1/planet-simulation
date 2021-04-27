using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundRover : MonoBehaviour
{
    public Transform roverTransform;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(roverTransform.position, Vector3.up, 20 * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(roverTransform.position, Vector3.down, 20 * Time.deltaTime);
        }
    }
}
