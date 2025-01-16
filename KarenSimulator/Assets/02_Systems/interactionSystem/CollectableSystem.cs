using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CollectableSystem : MonoBehaviour
{

    //Rigidbody rb;
    private void Start()
    {
       // rb = GetComponent<Rigidbody>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        Transform hand = other.GetComponent<PlayerInput>().playerHand;
        Instantiate(transform, hand);

    }
}
