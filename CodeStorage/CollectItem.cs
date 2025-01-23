using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))// only if player colides
        { 
            TrigerThis(other);
        }
    }
    void TrigerThis(Collider cl)
    {
        PlayerInputs player = cl.GetComponent<PlayerInputs>();
        if (player.itemInstance == null)
        {
            player.EquipItem(gameObject);
            Destroy(gameObject);
        }
    }
}
