using UnityEngine;


public class CollectableSystem : MonoBehaviour
{
    private PlayerInput player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            player = other.GetComponent<PlayerInput>();

            Debug.Log("collided");
            // Activate the equip function
            if (player.playerHand.childCount == 0 )
            {
                player.EquipItem(transform.gameObject);
             //   transform.parent.GetComponent<Spawner>().ItemWasPickedUP();
                Destroy(gameObject);
            }
            // Activate the equip function
        }

    }
    /*public void EquipItem(Transform newGameObject)
    {
        Debug.Log("Equipping");

        // Check if there are no children in the player's hand
        if (player.playerHand.childCount == 0)
        {
            // Instantiate the new item in the player's hand
            Transform Item = Instantiate(newGameObject, player.playerHand);
            Item.transform.localPosition = Vector3.zero;
            // Equip the new item
            player.itemEquiped = Item.GetComponent<Item>();

            // Debug: Log the new item that has been equipped
            Debug.Log($"New item equipped: {Item.name}");
        }
        else
        {
            Debug.LogWarning("Player hand is not empty. Can't equip a new item.");
        }
    }*/
}
