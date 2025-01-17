using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ResidentAi : MonoBehaviour
{
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
    }
}
