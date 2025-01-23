using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("A scriptable object that has a preset of items that it can spawn. Location OBJ_Prefabs")]
    public ItemSpawnList prefabLsit;
    [SerializeField] float itemRespawnDelay = 5f;
    [SerializeField] Transform spawnItemLocation;

    bool itemSpawned;
    bool isRespawning; // Tracks if the respawn timer is active
    int spawnIndex;
    GameObject thisItem;

    private void Awake()
    {
        itemSpawned = false;
        isRespawning = false;
    }

    private void Update()
    {
        if (!itemSpawned && thisItem == null && !isRespawning)
        {
            StartCoroutine(StartSpawnTimer());
        }
    }

    IEnumerator StartSpawnTimer()
    {
        isRespawning = true; // Prevent multiple coroutines from being started
        Debug.Log("Starting respawn timer...");
        yield return new WaitForSeconds(itemRespawnDelay);

        // Spawn the item after the timer
        SpawnItem();
        isRespawning = false; // Timer is no longer active
    }

    void SpawnItem()
    {
        // Check if the spawn list is valid
        if (prefabLsit == null || prefabLsit.allSpawnables.Length == 0)
        {
            Debug.LogError("No spawnable objects defined in the spawnables preset.");
            return;
        }

        // Set itemSpawned to true to prevent multiple spawns
        itemSpawned = true;
        Debug.Log("Spawning a new item.");

        // Randomly select an item to spawn
        spawnIndex = Random.Range(0, prefabLsit.allSpawnables.Length);

        // Instantiate the item at the spawn location
        thisItem = Instantiate(prefabLsit.allSpawnables[spawnIndex], spawnItemLocation);

        // Reset transform to align with the spawn location
        thisItem.transform.localPosition = Vector3.zero;
    }
}

