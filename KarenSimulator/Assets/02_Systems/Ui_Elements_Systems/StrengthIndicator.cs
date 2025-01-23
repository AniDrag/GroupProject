using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StrengthIndicator : MonoBehaviour
{

    public enum SliderType
    {
        Strenght,
        Danger
    }
    [Header("Slider settings")]
    public SliderType sliderType;
    public float fillSpeed;
    public bool isActive;
    public UnityEvent onDangerSliderFill;

    //debug
    Slider thisSlider;
    float fillIndex;

    // Update is called once per frame
    private void Awake()
    {
        thisSlider = GetComponent<Slider>();
        fillIndex = 100 / fillSpeed;
    }
    void Update()
    {
        if (isActive)
        {
            UpdateSlider();
        }
    }
    void UpdateSlider()
    {
        thisSlider.value += Time.deltaTime * fillIndex;
        //Danger slider condition
        if (sliderType == SliderType.Danger && thisSlider.value == thisSlider.maxValue)
        {
            onDangerSliderFill?.Invoke();// move playerHand position to other hand
            Debug.Log("hand got destroyed");
        }
        else if (sliderType == SliderType.Strenght)// Strenght slider value
        {
            if (thisSlider.value == thisSlider.maxValue || thisSlider.value == thisSlider.minValue)
            {
                fillIndex *= -1;
            }
        }
    }


}

    /*[SerializeField] Slider strengthMeter; // Reference to the slider
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
    }*/



