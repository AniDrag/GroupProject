using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Building : MonoBehaviour
{

    [Header("Residence Settings")]
    [SerializeField] int residenceCount; // Total number of residents
    [SerializeField] GameObject residencePrefab; // Resident prefab to spawn
    [SerializeField] Transform residenceSpawnLocation; // Spawn location for residents
    private GameObject[] residences; // Array to store spawned residents

    [Header("Annoyance Settings")]
    public int currentAnnoyance; // Current annoyance level
    public int maxAnnoyance; // Maximum annoyance level
    public bool isAnnoyed; // Whether the building is annoyed
    [SerializeField, Range(1, 20)] float annoyanceDecreaseTimer; // Timer for decreasing annoyance
    [SerializeField, Range(1, 10)] int decrementAnnoyance; // Annoyance decrement per interval

    private bool decreasedAnnoyance;
    private bool hasCitizensOutside;
    private bool checkingCitizens;

    [Header("Wave Settings")]
    private int waveAmount; // Annoyance threshold for the next wave
    private int oldWaveAmount; // Previous wave threshold
    private int currentWave; // Current wave number

    private void Start()
    {
        isAnnoyed = false;
        residences = new GameObject[residenceCount];
        waveAmount = maxAnnoyance / 5;
        currentWave = 0;
    }

    private void Update()
    {
        if (!checkingCitizens)
        {
            StartCoroutine(CheckIfCitizensAreOutsideOfHouse());
        }

        // Decrease annoyance if no residents are outside
        if (!decreasedAnnoyance && !hasCitizensOutside)
        {
            StartCoroutine(DecreaseAnnoyance());
        }

        // Handle annoyance-based waves
        if (currentAnnoyance >= waveAmount)
        {
            waveAmount += maxAnnoyance / 5;
            currentWave++;
            SpawnCitizens();
        }
        else if (currentAnnoyance < oldWaveAmount)
        {
            currentWave--;
            waveAmount -= maxAnnoyance / 5;
            oldWaveAmount -= maxAnnoyance / 5;
        }
    }

    /// <summary>
    /// Increases the building's annoyance by the given amount.
    /// </summary>
    public void AnnoyTarget(int annoyanceAmount)
    {
        currentAnnoyance += annoyanceAmount;
        if (currentAnnoyance >= maxAnnoyance)
        {
            isAnnoyed = true;
        }
    }

    /// <summary>
    /// Gradually decreases annoyance over time.
    /// </summary>
    private IEnumerator DecreaseAnnoyance()
    {
        decreasedAnnoyance = true;

        // Decrease annoyance by maxAnnoyance / residenceCount if residents are all inside
        if (!hasCitizensOutside)
        {
            currentAnnoyance -= Mathf.RoundToInt((float)maxAnnoyance / residenceCount);
            currentAnnoyance = Mathf.Max(0, currentAnnoyance); // Ensure annoyance doesn't go below zero
        }

        yield return new WaitForSeconds(annoyanceDecreaseTimer);
        decreasedAnnoyance = false;
    }

    /// <summary>
    /// Checks if any residents are currently outside the building.
    /// </summary>
    private IEnumerator CheckIfCitizensAreOutsideOfHouse()
    {
        checkingCitizens = true;
        hasCitizensOutside = false;

        for (int i = 0; i < residences.Length; i++)
        {
            if (residences[i] != null) // Check if a resident exists
            {
                hasCitizensOutside = true;
                break;
            }
        }

        yield return new WaitForSeconds(5);
        checkingCitizens = false;
    }

    /// <summary>
    /// Spawns citizens based on the current wave.
    /// </summary>
    private void SpawnCitizens()
    {
        int residentsToSpawn = (residenceCount / 5) * currentWave;

        for (int i = 0; i < residences.Length; i++)
        {
            if (residences[i] == null && residentsToSpawn > 0)
            {
                residences[i] = Instantiate(residencePrefab, residenceSpawnLocation.position, Quaternion.identity);
                residentsToSpawn--;
            }
        }
    }
    /*
    //
    //spawn ai
    //is anyoed
    //curent anoyance
    [Header("Residence Settings")]
    [SerializeField] int residenceCount;
    [SerializeField] GameObject residencPrefab;
    [SerializeField] Transform residenceSpawnLocation;
    private GameObject[] residence;

    [Header("Anoyance Settigns")]
    public int currentAnoyance;
    public int maxAnoyance;
    public bool isAnoyed;
    [SerializeField] [Range(1, 20)]float anoyanceDecreaseTimer;
    [SerializeField][Range(1, 10)] int decrementAnoyance;

    bool decreasedAnoyance;
    bool hasCitizensOutside;
    bool checkingCitizens;
    int newWaweAmount;
    int oldWaweAmount;
    int curentWawe;

    void Start()
    {
        isAnoyed = false;
        residence = new GameObject[residenceCount];
        newWaweAmount = maxAnoyance / 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkingCitizens)
        {
            StartCoroutine(CheckIfCitizensAreOutsideOfHouse());
        }
        //if no residence outside decrease anoyance
        if (!decreasedAnoyance && hasCitizensOutside)
        {
            StartCoroutine(DecreaseAnoyance());
        }



        if (currentAnoyance >= newWaweAmount)
        {
            newWaweAmount += maxAnoyance / 5;
            curentWawe++;
            SpawnCitizens();
        }
        else if (currentAnoyance < oldWaweAmount)
        {
            curentWawe--;
            newWaweAmount -= maxAnoyance / 5;
            oldWaweAmount -= maxAnoyance / 5;
        }
    }

    public void AnnoyeTarget(int anoyanceAmount)
    {
        currentAnoyance += anoyanceAmount;
        if (currentAnoyance >= maxAnoyance)
        {
            isAnoyed = true;
        }

    }
    IEnumerator DecreaseAnoyance()
    {
        decreasedAnoyance = true;
        currentAnoyance -= decrementAnoyance;
        yield return new WaitForSeconds(anoyanceDecreaseTimer);
        decreasedAnoyance = false;
    }

    IEnumerator CheckIfCitizensAreOutsideOfHouse()
    {
        // loop tghru the lenght
        // check if resident i is null
        checkingCitizens = true;
        for (int i = 0; i<= residenceCount; i++)
        {
            if(residence[i].gameObject == null)
            {
                hasCitizensOutside = false;
            }
            else
            {
                hasCitizensOutside = true; break;
            }
        }
        yield return new WaitForSeconds(5);
        checkingCitizens = false;


    }
    void SpawnCitizens()
    {
        // fill the residence spawn
        //make wawes so // by 5 and ech wawe spawns  one ore residence
        //
        int i = 0;
        if (residence[i].gameObject == null && i <= (residenceCount/5) * curentWawe)
        {
            residence[i] = Instantiate(residencPrefab, residenceSpawnLocation);
            i++;
            SpawnCitizens();
        }
    }*/
}
