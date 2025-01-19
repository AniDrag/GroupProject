using UnityEngine;
using UnityEngine.Events;

public class SwitchScreenOnPress : MonoBehaviour
{
    [Header("Events for Screen Switching")]
    [Tooltip("Event triggered when switching to the first screen (e.g., first-person view).")]
    [SerializeField] private UnityEvent onSwitchToFirstScreen;

    [Tooltip("Event triggered when switching to the second screen (e.g., third-person view).")]
    [SerializeField] private UnityEvent onSwitchToSecondScreen;

    [Header("Current POV State")]
    [Tooltip("Indicates the current point of view. True for first-person view, false for third-person view.")]
    public bool isFirstPersonView = true;

    /// <summary>
    /// Switches between two screen views based on the current POV state.
    /// </summary>
    public void SwitchScreen()
    {
        if (isFirstPersonView)
        {
            // Trigger event to switch to the second screen
            onSwitchToSecondScreen?.Invoke();
            isFirstPersonView = false; // Update state to third-person view
        }
        else
        {
            // Trigger event to switch to the first screen
            onSwitchToFirstScreen?.Invoke();
            isFirstPersonView = true; // Update state to first-person view
        }
    }



    /*
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

    }*/
}
