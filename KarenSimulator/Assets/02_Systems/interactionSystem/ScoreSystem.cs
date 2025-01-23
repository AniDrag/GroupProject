using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public SaveGameData Data; 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Annoyable"))
        {
            if(other.CompareTag("Annoyable"))
            {
                Debug.Log("collided");
            }
            Data.score += 10;
            Debug.Log("Score verhoogd via trigger! Huidige score: " + Data.score);

        }
    }
}
