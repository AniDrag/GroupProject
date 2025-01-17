using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Building : MonoBehaviour
{

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
    }
}
