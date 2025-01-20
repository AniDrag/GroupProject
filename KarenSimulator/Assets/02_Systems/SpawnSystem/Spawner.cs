using UnityEngine;


[CreateAssetMenu(fileName = "SpawnSetName", menuName = "Tools/Spawnable set")]
public class SpawnableObjects:ScriptableObject
{
    [Header("Spawnable Objects")]
    [Tooltip("All items that can be spawned.")]
    public GameObject[] allSpawnables;
}
public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private SpawnableObjects spawnablesPreset;
    [SerializeField, Tooltip("Delay before respawning an item after it is picked up.")] private float respawnDelay = 2f;
    [SerializeField, Tooltip("Height at which the item will spawn above the spawner."), Range(0, 5)] private float itemSpawnHeight = 1f;
    [SerializeField, Tooltip("Scale of the item when spawned.")] private Vector3 itemScaleOnSpawn = Vector3.one;

    [Header("Player Spawning")]
    [SerializeField, Tooltip("Check if this spawner is responsible for spawning the player.")]
    private bool isPlayerSpawner = false;
    [SerializeField, Tooltip("Player prefab to spawn (if applicable).")] private GameObject playerPrefab;

    // Internal tracking
    private GameObject spawnedItem;
    private Transform spawnerTransform;
    private bool isSpawningItem;
    private int spawnIndex;

    private void Awake()
    {
        spawnerTransform = transform;
        isSpawningItem = false;

        // Automatically spawn the player if this is a player spawner
        if (isPlayerSpawner)
        {
            SpawnPlayer();
        }
    }

    private void Update()
    {
        // If this is not a player spawner, handle item respawning
        if (!isPlayerSpawner && !isSpawningItem && spawnedItem == null)
        {
            isSpawningItem = true;
            Invoke("SpawnItem", respawnDelay);
        }
    }

    /// <summary>
    /// Spawns an item from the spawnables preset at the spawner's position.
    /// </summary>
    public void SpawnItem()
    {
        if (spawnablesPreset == null || spawnablesPreset.allSpawnables.Length == 0)
        {
            Debug.LogError("No spawnable objects defined in the spawnables preset.");
            return;
        }

        isSpawningItem = false;
        Debug.Log("Spawning a new item.");

        // Randomly select an item to spawn
        spawnIndex = Random.Range(0, spawnablesPreset.allSpawnables.Length);
        spawnedItem = Instantiate(
            spawnablesPreset.allSpawnables[spawnIndex],
            spawnerTransform.position + Vector3.up * itemSpawnHeight,
            Quaternion.identity
        );
        spawnedItem.transform.localScale = itemScaleOnSpawn;
    }

    /// <summary>
    /// Spawns the player at the spawner's position if no player exists.
    /// </summary>
    public void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned for this spawner.");
            return;
        }

        if (spawnedItem == null)
        {
            Debug.Log("Spawning the player.");
            spawnedItem = Instantiate(
                playerPrefab,
                spawnerTransform.position + Vector3.up * itemSpawnHeight,
                Quaternion.identity
            );
            spawnedItem.transform.localScale = Vector3.one; // Default scale for the player
        }
        else
        {
            Debug.LogWarning("Player spawn attempt blocked; another object is already spawned.");
        }
    }

    /*
    [SerializeField] SpawnableObjects spawnablesPreset;
    [SerializeField][Tooltip("after item is picked up")] float respawnDelay;
    [SerializeField][Tooltip("Height of item spawn")][Range(0, 5)] float itemSpawnHeight;
    [SerializeField][Tooltip("Item scale when spawned")][Range(0, 5)] Vector3 itemScaleOnSpawn;

    [Header("Player spawning")]
    [SerializeField] bool spawnPlayer;
    
    //store the item pawned
    GameObject spawnedItem;

    Transform spawnerTransform;
    bool invokedSpawn;
    int spawnIndex;//randomized for item spawning

    private void Awake()
    {
        spawnerTransform = gameObject.transform;
        invokedSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnPlayer && !invokedSpawn && spawnedItem == null)
        {
            invokedSpawn = true;
            Invoke( "SpawnItem", respawnDelay);
        }
    }
    public void SpawnItem()
    {            
        invokedSpawn = false;
        Debug.Log("No item, spawning Item");
        spawnIndex = Random.Range(0, spawnablesPreset.allSpawnables.Length);
        spawnedItem = Instantiate(spawnablesPreset.allSpawnables[spawnIndex], spawnerTransform.position + Vector3.up * itemSpawnHeight, Quaternion.identity);
        spawnedItem.transform.localScale = itemScaleOnSpawn;
        
        if (spawnedItem != null)
        {
            Debug.LogWarning("Spawning Item Error, spawned an item while tem exits");
        }
        
    }
    public void SpawnPlayer()
    {
        if (!spawnedItem)
        {
            //SpawnPlayerPrefab 
        }
    }*/
}
