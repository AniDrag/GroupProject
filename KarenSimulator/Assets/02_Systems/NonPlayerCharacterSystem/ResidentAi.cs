using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ResidentAi : MonoBehaviour
{
    [Header("AI Settings (for people to use)")]
    [SerializeField] float aiSpeed;
    [SerializeField] float playKillAnimationRange;
    [SerializeField] float attackSpeed;
    [SerializeField] int maxAnnoyance;
    [SerializeField] int currentAnnoyance;
    
    [Header("Events")]
    [Tooltip("Only SFX and VFX,")]
    [SerializeField] UnityEvent playOnAttack; // Only SFX and VFX
    [SerializeField] UnityEvent playOnTakeDamage; // Only SFX and VFX
    [SerializeField] UnityEvent playOnMaxAnoyance;

    [Header("References")]
    [SerializeField] Animator aiAnimator;
    [SerializeField] float attackAnimLength;
    Vector3 spawnPoint; // The point where the AI should return

    [Header("Debug Settings")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float spottingRange;
    [SerializeField] float attackRange;


    private NavMeshAgent aiAgent;
    private Transform target;
    private bool isAttacking;
    private bool playerFound;
    private bool checkDistance;

    private bool isReturningToSpawn = false; // To check if the AI is returning to spawn

    void Awake()
    {
        aiAgent = GetComponent<NavMeshAgent>();
        aiAgent.speed = aiSpeed; // Set AI speed to the specified value
        spawnPoint = transform.position;
    }

    void Update()
    {
        if (!playerFound && !isReturningToSpawn)
        {
            FindPlayer();
        }
        else if (!isReturningToSpawn)
        {
            ChasePlayer();
        }
    }

    void FindPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, spottingRange, playerLayer);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
            aiAnimator.SetBool("RunAtPlayer", true);
            playerFound = true;
        }
        else
        {
            Debug.LogWarning("No player found");
        }
    }

    void ChasePlayer()
    {
        if (target == null) return;

        aiAgent.SetDestination(target.position);

        if (!checkDistance)
            StartCoroutine(CheckChaseDistance());
    }

    IEnumerator CheckChaseDistance()
    {
        checkDistance = true;

        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            aiAgent.SetDestination(transform.position); // Stop moving to attack the player

            if (!isAttacking)
                StartCoroutine(AttackPlayer());
        }

        yield return new WaitForSeconds(1);
        checkDistance = false;
    }

    IEnumerator AttackPlayer()
    {
        aiAnimator.SetBool("AttackPlayer", true);
        isAttacking = true;

        playOnAttack?.Invoke(); // Trigger attack sound/effects

        yield return new WaitForSeconds(attackAnimLength);

        aiAnimator.SetBool("AttackPlayer", false);
        isAttacking = false;
    }

    public void TakeDamage(int annoyanceAmount)
    {
        currentAnnoyance += annoyanceAmount;

        // If annoyance exceeds maxAnnoyance, set to max
        if (currentAnnoyance >= maxAnnoyance)
        {
            currentAnnoyance = maxAnnoyance;
            playOnMaxAnoyance?.Invoke();
            // Optionally, add additional effects or behavior when annoyance maxes out
        }

        aiAnimator.SetBool("TakeDamage", true); // Trigger damage animation
        playOnTakeDamage?.Invoke(); // Trigger take damage sound/effects

        Invoke("ResetTakeDamage", 1f); // Reset damage animation after 1 second
    }

    void ResetTakeDamage()
    {
        aiAnimator.SetBool("TakeDamage", false); // Reset damage animation state
    }

    // New function: decreases annoyance over time
    public IEnumerator DecreaseAnnoyanceOverTime()
    {
        while (currentAnnoyance > 0)
        {
            currentAnnoyance -= 1; // Decrease annoyance by 1 each time
            yield return new WaitForSeconds(1f); // Wait for 1 second before decreasing again
        }
        currentAnnoyance = 0; // Ensure annoyance doesn't go below 0
        Debug.Log("Annoyance fully decreased");

        // After decreasing annoyance, start returning to spawn
        StartCoroutine(ReturnToSpawn());
    }

    // New function: return to spawn point and disappear after
    IEnumerator ReturnToSpawn()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn point not set for the Resident!");
            yield break; // Exit if no spawn point is set
        }

        isReturningToSpawn = true;
        aiAgent.SetDestination(spawnPoint);

        // Wait until the AI reaches its spawn point
        while (Vector3.Distance(transform.position, spawnPoint) > 1f)
        {
            yield return null; // Continue checking position until close enough
        }

        // Once reached, deactivate or destroy the AI
        Debug.Log("Resident reached spawn point, disappearing.");
        gameObject.SetActive(false); // Deactivates the AI GameObject

        // Optionally, destroy the AI instead
        // Destroy(gameObject);

        yield break;


    }


    /*
    [Header("Ai settings (for people to use)")]
    [SerializeField] float aiSpeed;
    [SerializeField] float playKillAnimationRange;
    [SerializeField] float attackSpeed;
    [SerializeField] [Tooltip("Only SFX and VFX")] UnityEvent playOnAttack;
    [SerializeField][Tooltip("Only SFX and VFX")]  UnityEvent playOnTakeDamage;
    [SerializeField] int maxAnoyance;
    [SerializeField] int currentAnoyance;

    [Header("Refrences")]
    [SerializeField] Animator aiAnimator;
    [SerializeField] float attackAnimLenght;

    [Header("Debug settings")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float spottigRange;
    [SerializeField] float attackRange;
    private NavMeshAgent aiAgent;
    private Transform target;
    private bool isAttacking;
    private bool plaerFound;
    private bool checkDistance;
    
   void Awake()
    {
        aiAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (!plaerFound)
        {
            FindPlayer();
        }
        ChasePlayer();
    }

    void FindPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, spottigRange, playerLayer);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
            aiAnimator.SetBool("RunAtPlayer", true);
            plaerFound = true;

        }
        else
        {
            Debug.LogWarning("No player found");
        }
    }
    void ChasePlayer()
    {
        if (target == null) { return; }
        aiAgent.SetDestination(target.position);
        if(!checkDistance)
        StartCoroutine(CheckChaseDistance());
    }
    IEnumerator CheckChaseDistance()
    {
        checkDistance = true;
        if(Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            aiAgent.SetDestination(transform.position);
            
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
            
        }
        yield return new WaitForSeconds(1);
        checkDistance = false;
    }

    IEnumerator AttackPlayer()
    {
        aiAnimator.SetBool("AttackPlayer", true);
        isAttacking = true;
        playOnAttack?.Invoke();
        yield return new WaitForSeconds(attackAnimLenght);
        aiAnimator.SetBool("AttackPlayer", false);
        isAttacking = false;

    }

    public void TakeDamage( int anoyanceAmount)
    {
        currentAnoyance += anoyanceAmount;
        if(currentAnoyance >= maxAnoyance)
        {
            currentAnoyance = maxAnoyance;
            //PlayeEvent or add more pointss
        }
        aiAnimator.SetBool("TakeDamage", true);
        Invoke("ResetTakeDamage", 1f);
        playOnTakeDamage?.Invoke();
    }
    void ResetTakeDamage()
    {
        aiAnimator.SetBool("TakeDamage", false);
    }*/
}
