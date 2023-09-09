using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyManager : MonoBehaviour

    
{
    /*----------------PUBLIC VARIABLES-----------------------------*/
    
    // Quick singleton, I have no control over duplicates
    public static TransparencyManager Instance;

    // Direction of elevator movement
    public bool up;

    /*----------------PRIVATE VARIABLES-----------------------------*/

    // Transparency levels
    private int currentRailingsTransparency, currentFloorTransparency, currentElevatorTransparency;

    // Material references
    [SerializeField] private Material railingsMaterial, floorMaterial, elevatorMaterial;

    // Values to manage the gradual transparency change during elevator ascent or descent.
    private float actualTransparency, nextTransparency;

    // DifficultyLevel
    private enum DifficultyLevel
    {
        Easy, Medium, Hard
    }

    [SerializeField] private DifficultyLevel difficultyLevel;

    /*----------------INITIALIZATION METHODS-----------------------------*/

    // The singleton allows it to be used directly from outside using TransparencyManager.instance. I can use all the public methods that belong to it.
    private void Awake()
    {
        Instance = this;
    }

    //retrieved from game manager to update transparency status
    public void Start()
    {
        // Initialize transparency levels for Elevator
        currentElevatorTransparency = 0;

        // Update lever rotations and material opacities
        SetMaterialAlpha(elevatorMaterial, currentElevatorTransparency);
    }

    /*----------------GENERAL METHODS-----------------------------*/ 

    // Changes the transparency of the railings
    public void SetRailingsTransparency(int transparency)
    {
    currentRailingsTransparency = transparency;
    SetMaterialAlpha(railingsMaterial, currentRailingsTransparency);
    }

    // Changes the transparency of the floor
    public void SetFloorTransparency(int transparency)
    {
    currentFloorTransparency = transparency;
    SetMaterialAlpha(floorMaterial, currentFloorTransparency);
    }

    // Changes the transparency of the elevator
    public void SetElevatorTransparency(int transparency)
    {
        currentElevatorTransparency = transparency;
        SetMaterialAlpha(elevatorMaterial, currentElevatorTransparency);
    }

    // Changes the transparency of the material
    public void SetMaterialAlpha(Material myMaterial, int transparency)
    {
        //changes the alpha value of the material based on the current value of transparency, after making a conversion
        Color color = myMaterial.color;

        // Set float value base on difficulty level
        float value;
        switch (difficultyLevel)
        {
            case DifficultyLevel.Easy:
                value = 500f;
                break;
            case DifficultyLevel.Medium:
                value = 350f;
                break;
            case DifficultyLevel.Hard:
                value = 200f;
                break;
            default:
                value = 500f;
                break;
        }

        // Set transparency
        color.a = (float)(1-(transparency/value));
        myMaterial.color = color;
    }

    /*----------------ELEVATOR METHODS-----------------------------*/ 

    // Change the transparency of the elevator gradually from one floor to another, it is called by an event in the animation.
    public void ChangeElevatorTransparency(int from)
    {
        float animationDuration = 12f;

        switch (from)
        {

            case 1: 
                actualTransparency = 0;
                nextTransparency = 0.5f;
               
                break;
            case 2:
                if (up)
                {
                    actualTransparency = 0.5f;
                    nextTransparency = 1.1f;
                }
                else
                {
                    actualTransparency = 0.5f;
                    nextTransparency = 0;
                }
                break;
            case 3:
                if (up)
                {
                    actualTransparency = 1.1f;
                    nextTransparency = 1.8f;
                }
                else
                {
                    actualTransparency = 1.1f;
                    nextTransparency = 0.5f;
                }
                break;
            case 4:
                if (up)
                {
                    actualTransparency = 1.8f;
                    nextTransparency = 3f;
                }
                else
                {
                    actualTransparency = 1.8f;
                    nextTransparency = 1.25f;
                }
                break;
            case 5:
                if (up)
                {
                    actualTransparency = 3f;
                    nextTransparency = 3.6f;
                }
                else
                {
                    actualTransparency = 3f;
                    nextTransparency = 1.8f;
                }
                break;
            case 6:
                actualTransparency = 3.6f;
                nextTransparency = 3f;
                break;
            default:
                break;
        }

        StartCoroutine(TransparencyCoroutine(actualTransparency, nextTransparency, animationDuration));
    }

    /*
     I use a coroutine to gradually increase transparency over time. The coroutine uses the Mathf.Lerp function to interpolate the transparency gradually,
     based on the elapsed time and the total duration of the animation.
         */
    private IEnumerator TransparencyCoroutine(float at, float nt, float animationDuration)
    {
        float startTime = Time.time;
        float endTime = startTime + animationDuration;
        float startTransparency = at;

        int savedValue = 0;
        while (Time.time <= endTime)
        {
            float t = (Time.time - startTime) / animationDuration;
            int mappedTransparency = (int)Mathf.Lerp(startTransparency, nt, t);
            savedValue = mappedTransparency;
            SetElevatorTransparency(mappedTransparency);
            yield return null;
        }

        SetElevatorTransparency(savedValue);
    }
}
