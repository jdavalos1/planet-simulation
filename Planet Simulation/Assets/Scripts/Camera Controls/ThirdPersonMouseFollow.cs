using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMouseFollow : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * 200 * Time.deltaTime;

        transform.RotateAround(player.position, Vector3.up, -mouseX);
    }
}
