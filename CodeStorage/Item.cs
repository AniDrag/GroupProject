using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Consumable,
        Throwable
    }

    public enum EffectLayer
    {
        All,
        Buildings,
        People
    }

    [Header("Item settings")]
    public ItemType itemType;
    public EffectLayer effectLayer;
    [SerializeField] float damageZone;
    [SerializeField] int damageAmount;
    [SerializeField] float itemMass;

    // Debug
    Rigidbody itemBody;
    SphereCollider itemCollider;
    public bool itemThrown;

    private void Awake()
    {
        itemBody = GetComponent<Rigidbody>();
        itemBody.isKinematic = true;
        itemBody.mass = itemMass;
        itemCollider = GetComponent<SphereCollider>();
        itemCollider.isTrigger = true;
    }

    public void ThrowItem()
    {
        itemBody.isKinematic = false;
        itemCollider.isTrigger = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Apply damage within the radius of the item impact
        ApplyDamageInRange();

        Destroy(gameObject); // Destroy after impact
    }

    private void ApplyDamageInRange()
    {
        // Perform a sphere overlap check around the item's current position
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageZone);

        foreach (var hitCollider in hitColliders)
        {
            if (ShouldAffectLayer(hitCollider.gameObject.layer))
            {
                Debug.Log($"Item hit: {hitCollider.gameObject.name}");

                // Apply damage to building or resident
                if (hitCollider.gameObject.TryGetComponent<Building>(out Building building))
                {
                    building.AnnoyTarget(damageAmount); // Call the AnnoyTarget method for Buildings
                }
                else if (hitCollider.gameObject.TryGetComponent<ResidentAi>(out ResidentAi residentAI))
                {
                    residentAI.TakeDamage(damageAmount); // Call the TakeDamage method for People
                }
            }
            else
            {
                Debug.Log($"Item ignored: {hitCollider.gameObject.name}");
            }
        }
    }

    private bool ShouldAffectLayer(int layer)
    {
        // Check the layer of the collided object based on the EffectLayer
        int buildingsLayer = LayerMask.NameToLayer("Buildings");
        int peopleLayer = LayerMask.NameToLayer("People");

        switch (effectLayer)
        {
            case EffectLayer.All:
                // Only hit Buildings and People layers
                return layer == buildingsLayer || layer == peopleLayer;
            case EffectLayer.Buildings:
                // Hit only Buildings
                return layer == buildingsLayer;
            case EffectLayer.People:
                // Hit only People
                return layer == peopleLayer;
            default:
                return false;
        }
    }
}
