using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class StrengthIndicator : MonoBehaviour
{
    [SerializeField] Slider strengthMeter; // Reference to the slider
    public float speedMeter = 1f; // Speed of movement

    public bool isMoving;
    [Range(0.1f, 10)]
    public float speedTransfer;
    public bool isActive;
    private bool isMovingRight;
    public enum MeterType
    {
        none,
        strengthmeter, 
        dangermeter
    }

    public MeterType meterType;  
    private void Start()
    {
        speedMeter = 100 / speedTransfer;

    }
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (meterType == MeterType.strengthmeter)
        {
            StrengthMeter();
        }

        else if (meterType == MeterType.dangermeter)
        {
            DangerMeter();
        }

        else
        {
            Debug.LogWarning("No sliderType selected");
        }

    }
    void StrengthMeter()
    {
        if (strengthMeter == null) return;
        // Move the slider value
        if (isMovingRight)
        {
            strengthMeter.value += speedMeter * Time.deltaTime;
            if (strengthMeter.value >= strengthMeter.maxValue)
            {
                isMovingRight = false;
            }

        }
        else
        {
            strengthMeter.value -= speedMeter * Time.deltaTime;
            if (strengthMeter.value <= strengthMeter.minValue)
            {
                isMovingRight = true;
            }

        }


    }

    void DangerMeter()
    {
        strengthMeter.value += speedMeter * Time.deltaTime;
        if (strengthMeter.value == strengthMeter.maxValue)
        {
            Debug.Log("Destroy hand");
        }
    }
}
