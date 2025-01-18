using UnityEngine;


public class CollectableSystem : MonoBehaviour
{
    private PlayerInput player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerInput>();

            if (player.itemEquiped == null)
            {
                Destroy(gameObject);
                player.itemEquiped = transform.GetComponent<Item>();    
                Instantiate(transform, player.playerHand);
            }

        }
    }
}
