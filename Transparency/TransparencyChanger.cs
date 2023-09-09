using Autohand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyChanger : MonoBehaviour
{
    /*----------------PRIVATE VARIABLES-----------------------------*/

    // Material type
    private enum MaterialType
    {
        Floor,Railing
    }

    [SerializeField] private MaterialType materialType;

    // Reference to the transparency slider text
    [SerializeField] private TMPro.TextMeshPro text;    

    // Reference to the transparency slider
    [SerializeField] private PhysicsGadgetHingeAngleReader sliderReader;

    // Previous slider value 
    private float previousSliderValue = float.MinValue;

    /*---------------- METHODS-----------------------------*/

    void Update()
    {
        // Check if the slider reader is valid
        if (sliderReader == null)
        {
            // Return early if the slider reader is null
            return;
        }

        // Get the current slider value
        float currentSliderValue = sliderReader.GetValue();

        // Check if the current slider value is different from the previous one
        if (Mathf.Approximately(currentSliderValue, previousSliderValue) == false)
        {
            // Update the previous slider value
            previousSliderValue = currentSliderValue;

            // Convert the slider value to an integer between 0 and 100
            int currentTransparency = Mathf.RoundToInt((currentSliderValue + 1f) / 2f * 100f);
            currentTransparency = Mathf.Clamp(currentTransparency, 0, 100);

            // Set the transparency using the TransparencyManager
            if (materialType == MaterialType.Floor)
            {
                TransparencyManager.Instance.SetFloorTransparency(currentTransparency);
            }
            else if (materialType == MaterialType.Railing)
            {
                TransparencyManager.Instance.SetRailingsTransparency(currentTransparency);
            }

            // Update the text with the current transparency value
            text.text = currentTransparency.ToString();
        }
    }
    
}


