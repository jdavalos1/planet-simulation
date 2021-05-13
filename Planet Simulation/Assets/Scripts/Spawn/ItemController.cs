using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the spawn of the items
public class ItemController : MonoBehaviour
{
    public Vector3 maxPositions;
    public GameObject itemToSpawn;
    [Min(0)]
    public int maxItemSpawns;
    public float timeForNextSpawn;

    private List<GameObject> itemsNearPlayer;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        itemsNearPlayer = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time >= timeForNextSpawn)
        {
            CreateSpawn();
        }

        // Remove all items that no longer exist
        RemoveEmpty();
    }

    void CreateSpawn()
    {
        time = 0;
        if (itemsNearPlayer.Count >= maxItemSpawns) return;
    }
    
    void RemoveEmpty()
    {
        itemsNearPlayer.RemoveAll(item => item == null);
    }
}
