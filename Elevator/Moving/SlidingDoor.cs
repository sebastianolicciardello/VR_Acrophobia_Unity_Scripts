using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    /*----------------PRIVATE VARIABLES-----------------------------*/

    // Reference to the animator for controlling the door animation
    [SerializeField] private Animator anim;

    // Reference to the player's transform
    [SerializeField] private Transform player;

    // Reference to the door's transform
    [SerializeField] private Transform door;

    // Minimum distance between the player and the door to activate the animation
    [SerializeField] private float distanceFromPlayer;

    /*---------------- METHODS-----------------------------*/
    void Update()
    {
        // Calculate the squared distance between the player and the door
        float distance = (player.position - door.position).sqrMagnitude;

        // Check if the player is close enough to the door and if the currentLevel is 0
        if (distance <= distanceFromPlayer * distanceFromPlayer && ButtonsManager.Instance.numberButtonPressed == 0)
        {
            // Set the "Near" parameter of the animator to true to activate the door animation
            anim.SetBool("Near", true);
            
        }
        else
        {
            // Set the "Near" parameter of the animator to false to deactivate the door animation
            anim.SetBool("Near", false);
            
        }
    }


}
