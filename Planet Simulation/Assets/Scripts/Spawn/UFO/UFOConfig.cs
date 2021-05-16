using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOConfig : MonoBehaviour
{
    public float bobSpeed;
    public float bobHeight;
    public float maxPlayerDistance;
    public string ufoMusic;

    private float originalY;
    private AudioManager playerAudio;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerAudio = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        bool outOfBounds = CheckShipOutOfBounds();
        // If it's out of bounds kill it
        if(outOfBounds)
        {
            playerAudio.audioLock = false;
            Destroy(gameObject);
        }
        else
        {
            if (!playerAudio.audioLock)
            {
                playerAudio.StopAll();
                playerAudio.Play(ufoMusic);
                playerAudio.audioLock = true;
            }

            // Bob the ship over time
            BobShip();
        }
    }

    void BobShip()
    {
        transform.position = new Vector3(transform.position.x,
                                         originalY + Mathf.Sin(Time.time * bobSpeed) * bobHeight,
                                         transform.position.z);
    }
    bool CheckShipOutOfBounds()
    {
        return Vector3.Distance(player.position, transform.position) > maxPlayerDistance;
    }
}
