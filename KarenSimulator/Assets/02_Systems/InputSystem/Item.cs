using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        None,
        Throwable,
        Consumable
    }
    public enum EfectLayer
    {
        None,
        Buildings,
        Residents,
        All
    }
    public enum DangerActivator
    {
        None,
        OnEquip,
        OnAim
    }
    [Header("Item settigns")]
    public string itemName;
    public ItemType itemType;
    public DangerActivator dangerActivator;
    [Tooltip("The object this item can effect")]
    public EfectLayer effectLayer;
    public int annoyanceAmount;
    [Tooltip("The radious of effect when item effects start")]
    public float annoyanceRadius;
    public float itemRange;// on the meeter if at max strenght this value is max strenght
                           //Debug
    [SerializeField] Transform _Debug_spawnAreaOffEffect;
    [SerializeField] Rigidbody _Debug_itemBody;//the Rigid body component
    [SerializeField] Collider _Debug_itemCollider; // if we have items on a seperate layer this isnt an issue
                                                   //MeeterUI playerMeeter; player Refrences Script will be attached here. all the players refrences will be here.
    Collider[] hits;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            CheckHitLayer();
        }
    }
    void ActivateItem()
    {
        _Debug_itemCollider.isTrigger = false;
    }

    void CheckHitLayer()
    {
        if (dangerActivator == DangerActivator.None)
        {
            hits = Physics.OverlapSphere(transform.position, annoyanceRadius);
            if (effectLayer == EfectLayer.All)
            {
                HitBuildingsAndNPCs();
            }
            else if (effectLayer == EfectLayer.Buildings)
            {
                HitBuildings();
            }
            else if (effectLayer == EfectLayer.Residents)
            {
                HitResidence();
            }
            else
            {
                Debug.Log("Some kind of effect or something it hits no target but player");
            }
        }

    }
    void HitBuildingsAndNPCs()
    {
        foreach (var i in hits)
        {

            if (i.GetComponent<Building>() != null)
            {
                i.GetComponent<Building>().AnnoyTarget(annoyanceAmount);
            }
            else if (i.GetComponent<ResidentAi>() != null)
            {
                i.GetComponent<ResidentAi>().TakeDamage(annoyanceAmount);
            }
            else
            {
                Debug.Log("Item does not fit criteria");
            }
        }
    }
    void HitBuildings()
    {
        foreach (var i in hits)
        {

            if (i.GetComponent<Building>() != null)
            {
                i.GetComponent<Building>().AnnoyTarget(annoyanceAmount);
            }
        }
    }
    void HitResidence()
    {
        foreach (var i in hits)
        {

            if (i.GetComponent<ResidentAi>() != null)
            {
                i.GetComponent<ResidentAi>().TakeDamage(annoyanceAmount);
            }
        }
    }
}
    /* public enum ItemType
     {
         none,
         throwable,
         consumable
     }

     public ItemType itemType;
     public int annoyenceLevel = 1;
     public float annoyenceScale;
     public bool isPickedUp;

     [Range(0, 50)]
     public float radious;
     public enum ItemType
     {
         None,
         Throwable,
         Consumable
     }

     public enum ThrowableType
     {
         None,
         Snowball,
         Stone,
         Firecracker,
         Firework,
         Airhorn
     }
     public enum EfectLayer
     {
         None,
         Buildings,
         Residents,
         All
     }
     [Header("Item settigns")]
     public string itemName;
     public ItemType itemType;
     [Tooltip("The object this item can effect")]
     public EfectLayer effectLayer;
     public int annoyanceAmount;
     [Tooltip("The radious of effect when item effects start")]
     public float annoyanceRadius;
     public float itemRange;// on the meeter if at max strenght this value is max strenght


     //Debug
     [SerializeField] Transform _Debug_spawnAreaOffEffect;
     [SerializeField] Rigidbody _Debug_itemBody;//the Rigid body component
     [SerializeField] Collider _Debug_itemCollider; // if we have items on a seperate layer this isnt an issue
     //MeeterUI playerMeeter; player Refrences Script will be attached here. all the players refrences will be here.*/



