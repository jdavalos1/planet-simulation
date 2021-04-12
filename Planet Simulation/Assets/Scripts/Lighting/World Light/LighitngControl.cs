using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighitngControl : MonoBehaviour
{
    public float timePass;

    public float currentTime;

    public float lengthOfDay;

    //public Direction sunrise;

    private float currentRotation;
    
    void Start()
    {
        currentRotation = 360 * currentTime / lengthOfDay + 180;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += timePass / lengthOfDay * Time.deltaTime;

        if (currentRotation >= 360.0f)
        {
            currentRotation = 0.0f;
        }

        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);
    }
}


[SerializeField]
public enum Direction
{
    North,
    South,
    East,
    West,
    None
}
