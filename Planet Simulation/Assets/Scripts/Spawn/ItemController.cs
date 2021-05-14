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
    public bool randomSpawnTime;
    public Transform player;

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
        timeForNextSpawn = randomSpawnTime ? Random.Range(0, timeForNextSpawn) : timeForNextSpawn;
        if (itemsNearPlayer.Count >= maxItemSpawns) return;

        var randPosition = new Vector3(Random.Range(-maxPositions.x, maxPositions.x),
                                       Random.Range(0, maxPositions.y),
                                       Random.Range(-maxPositions.z, maxPositions.z));
        var newItem = Instantiate(itemToSpawn, randPosition + player.position, Quaternion.identity);

        itemsNearPlayer.Add(newItem);
    }
    
    void RemoveEmpty()
    {
        itemsNearPlayer.RemoveAll(item => item == null);
    }
}
