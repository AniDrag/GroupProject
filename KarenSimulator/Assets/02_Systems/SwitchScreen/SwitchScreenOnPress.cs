using UnityEngine;
using UnityEngine.Events;

public class SwitchScreenOnPress : MonoBehaviour
{
    [SerializeField] UnityEvent switchOne;
    [SerializeField] UnityEvent switchTwo;
    public bool POV = true;
    public void SwitchScreen()
    {
        if (POV)
        {
            switchOne?.Invoke();
            POV = false;
        }
        else
        {
            switchTwo?.Invoke();   
            POV = true;
        }

    }
}
