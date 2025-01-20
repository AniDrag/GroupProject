using System;
using UnityEngine;
using UnityEngine.UI;

public class StrengthIndicator : MonoBehaviour
{
    [SerializeField] Slider strengthMeter; // Reference to the slider
    public float speedMeter = 1f; // Speed of movement
    private bool movingRight = true; // Direction flag
    public bool isMoving;
    [Range(0.1f, 10)]
    public float speedTransfer;
    private void Start()
    {
        speedMeter = 100 / speedTransfer;
    }
    void Update()
    {

        strengthMeter.value += speedMeter * Time.deltaTime;
        if(strengthMeter.value == strengthMeter.maxValue)
        {
            //explode hand
            //Debug.Log("animation ExplodeHand is playing");
        }

    }
}