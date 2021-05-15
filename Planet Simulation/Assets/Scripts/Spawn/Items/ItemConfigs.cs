using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConfigs : MonoBehaviour
{
    private readonly float maxEuclidDist = 100f;
    private readonly float bobSpeed = 1f;
    private readonly float bobHeight = 0.25f;
    private float originalY;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        BobItem();
        if (CheckItemOutOfBounds())
        {
            Destroy(gameObject);
        }
    }

    void BobItem()
    {
        transform.position = new Vector3(transform.position.x, 
                                originalY + Mathf.Sin(Time.time * bobSpeed) * bobHeight,
                                transform.position.z);
    }

    bool CheckItemOutOfBounds()
    {
        return Vector3.Distance(player.position, transform.position) > maxEuclidDist;
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Calling");
    }
}
