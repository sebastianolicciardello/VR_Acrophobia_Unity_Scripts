using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour{

    /*----------------PRIVATE VARIABLES-----------------------------*/

    // GameObject representing the activated and the deactivated image of the button
    [SerializeField] private GameObject activateImage;
    [SerializeField] private GameObject deactivateImage;

    // Number of the button
    [SerializeField] private int buttonNumber;

    // Boolean indicating whether the button is active or not
    private bool active = false;

    /*---------------- METHODS-----------------------------*/
    
    // Activates the button
    public void Activate() {
        active = true;
        activateImage.SetActive(true);
        deactivateImage.SetActive(false);
    }

    // Deactivates the button, called into Elevator Manager
    public void Deactivate() {
        active = false;
        activateImage.SetActive(false);
        deactivateImage.SetActive(true);
    }

    // Toggles the state of the button, called by Auto Hand Event for the touch of button
    public void Toggle() {
        // If the button is not active, activate it
        if (!active)
        {
            // Plays the button sound
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
            
            // Calls the methods of the "ElevatorManager" instance
            ElevatorManager.Instance.MovingElevator();
            Activate();
            ButtonsManager.Instance.ActivateButton(buttonNumber);
        }
            
    }
}
