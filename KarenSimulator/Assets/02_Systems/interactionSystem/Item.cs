using UnityEngine;

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
    //MeeterUI playerMeeter; player Refrences Script will be attached here. all the players refrences will be here.
}
