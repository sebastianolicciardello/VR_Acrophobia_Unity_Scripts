using UnityEngine;
using Autohand;
using Autohand.Demo;

public class ElevatorManager : MonoBehaviour

    
{   
    /*----------------PUBLIC VARIABLES-----------------------------*/

    // Quick singleton, I have no control over duplicates
    public static ElevatorManager Instance; 

    // Reference to ovrplayer
    public AutoHandPlayer playerController;
    // Reference to teleport controller
    public OVRTeleporterLink teleportController;

    // Reference to Sounds Clips
    public AudioClip liftSound, arrivalSound;

    // Reference to AudioManager
    public AudioManager audioManager;

    // Boolean indicating that the elevator has not yet been used since the start of the game
    public bool used = false;

    // Boolean indicating that the main menu is open
    public bool mainMenuActive = true;

    /*----------------PRIVATE VARIABLES-----------------------------*/
 
    // Takes references to doors and Player Camera
    [SerializeField] private GameObject doorLeft, doorRight;
    private ElevatorMovingVRCamera ElevatorVRCamera;

    /*----------------INITIALIZATION METHODS-----------------------------*/

    // The singleton allows it to be used directly from outside using ElevatorManager.instance I can use all the public methods that belong to it.
    private void Awake()
    {
        Instance = this;
    }

    // Save animator and script reference
    private void Start()
    {
        ElevatorVRCamera = GetComponent<ElevatorMovingVRCamera>();
    }

    /*----------------PUBLIC METHODS-----------------------------*/   

    // Retains the reference to the number pressed and whether we have an up or down animation
    // Called by event from animation
    public void UpDirection(int direction)
    {
        TransparencyManager.Instance.up = (direction == 1);
    }

    // Disable all touching buttons and close doors
    public void MovingElevator()
    {
        // Disable OVR movement and teleport
        playerController.useMovement = false;
        teleportController.enabled = false;

        // Close doors before moving
        CloseDoors();
    }

    /*---------------- METHODS CALLED BY EVENT FROM ANIMATION-----------------------------*/   

    // Enable all touching buttons and open doors
    public void NoMovingElevator(int currentFloor)
    {
        // Enable OVR movement and teleport
        playerController.useMovement = true;
        teleportController.enabled = true;

        // Enable buttons
        ButtonsManager.Instance.EnableButtons();

        //I manage which doors to open based on the floor I am on
        OpenDoors(currentFloor);
    }

    // Play lift sound when moving, play arrival sound when arrived
    public void PlaySoundLift(int floor)
    {
        bool arrived = false;
        if (arrived)
        {
            // not play sound lift
        }
        else
        {
            // set true if floor is the target floor
            if (floor == ButtonsManager.Instance.numberButtonPressed)
            {
                arrived = true;
            }
      
            if (!mainMenuActive){
                // load lift sound
                audioManager.PlayClip(liftSound);
            }

        }
       
    }

    // Play arrival sound when arrived
    public void PlaySoundArrival()
    {
        // Only allows the arrival sound to be played if the elevator has been used, otherwise it would also sound at the start of the game
        if (used == true && !mainMenuActive) 
        {
             // Play arrival sound
            audioManager.PlayClip(arrivalSound);
        }
    }

    // Change transparency while moving
    public void ChangeTransparency( int from)
    {
        TransparencyManager.Instance.ChangeElevatorTransparency(from);
    }

    /*----------------PRIVATE METHODS-----------------------------*/

    // I manage the closing of the doors before moving the elevator
    private void CloseDoors()
    {
        // Close doors
        doorLeft.GetComponent<Animator>().SetBool("Near", false);
        doorRight.GetComponent<Animator>().SetBool("Open", false);
        doorLeft.GetComponent<SlidingDoor>().enabled = false;
    }
    private void OpenDoors(int currentFloor)
    {
        if (ButtonsManager.Instance.numberButtonPressed == currentFloor & currentFloor != 0)
        {
            doorRight.GetComponent<Animator>().SetBool("Open", true);
        }
        else if (ButtonsManager.Instance.numberButtonPressed == currentFloor & currentFloor == 0)
        {
            doorLeft.GetComponent<SlidingDoor>().enabled = true;
            doorLeft.GetComponent<Animator>().SetBool("Near", true);
        }
    }
    
}
