using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovingVRCamera : MonoBehaviour
{

    /*---------------- VARIABLES-----------------------------*/    

    // Reference to ovrplayer
    [SerializeField] private Transform player;

    // Boolean indicating that player transform should follow the elevator because it is moving
    private bool shouldFollowElevator = false;

    /*----------------INITIALIZATION METHODS-----------------------------*/
    
    // In the start of the game the player transform does not follow the elevator
    void Start()
    {
        shouldFollowElevator = false;
    }

    /*---------------- METHODS-----------------------------*/

    // Called by an event from animation
    public void StopElevator()
    {
       shouldFollowElevator = false;
    }

    // Called by an event from animation
    //In this way, when the elevator moves, the player will move along with it
    public void StartElevator()
    {
        shouldFollowElevator = true;
    }

    // LateUpdate is called after Update 
    void LateUpdate()
    {
        if (shouldFollowElevator)
        {
            player.parent = transform;
        }
        else
        {
            player.parent = null;
        }
    }   

}
