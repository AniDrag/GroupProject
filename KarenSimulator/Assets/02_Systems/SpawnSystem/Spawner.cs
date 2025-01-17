using UnityEngine;


[CreateAssetMenu(fileName = "SpawnSetName", menuName = "Tools/Spawnable set")]
public class SpawnableObjects:ScriptableObject
{
    [Tooltip("All items it can spawn")]
    public GameObject[] allSpawnables;
}
public class Spawner : MonoBehaviour
{
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
    }
}
