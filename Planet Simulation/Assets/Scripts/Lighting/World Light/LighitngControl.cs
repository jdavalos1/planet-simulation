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
    private GameObject sunLight;
    
    void Start()
    {
        currentRotation = 360 * currentTime / lengthOfDay - 90;
        transform.localRotation = Quaternion.Euler(currentRotation, 0f, 0f);

        sunLight = GameObject.Find("Lighting/Day Light");
        HandleSunView();
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
        HandleSunView();
    }

    private void HandleSunView()
    {
        if (sunLight.activeSelf)
        {
            if (transform.eulerAngles.x > 170f && transform.eulerAngles.x < 345f)
            {
                sunLight.SetActive(false);
            }
        }
        else
        {
            if(transform.eulerAngles.x <= 170f || transform.eulerAngles.x >= 345f)
            {
                sunLight.SetActive(true);
            }
        }
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
