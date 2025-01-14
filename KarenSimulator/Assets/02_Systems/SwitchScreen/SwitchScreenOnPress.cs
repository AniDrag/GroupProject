using UnityEngine;
using UnityEngine.Events;

public class SwitchScreenOnPress : MonoBehaviour
{
    public UnityEvent switchOne;
    public UnityEvent switchTwo;
    bool switched = false;
    public void SwitchScreen()
    {
        if (!switched)
        {
            switchOne?.Invoke();
            switched = true;
        }
        else
        {
            switchTwo?.Invoke();   
            switched = false;
        }

    }
}
