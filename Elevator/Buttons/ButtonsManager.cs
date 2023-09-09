using System.Collections;
using Autohand;
using TurnTheGameOn.SimpleTrafficSystem;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    /*----------------PUBLIC VARIABLES-----------------------------*/

    // Quick singleton, I have no control over duplicates
    public static ButtonsManager Instance; 

    // Button pressed by the player
    public int numberButtonPressed;

    // To change position and rotation of levers
    public Vector3 newPositionLevers;
    public Quaternion newRotationLevers;

    // References to levers prefab
    public GameObject levers; 

    /*----------------PRIVATE VARIABLES-----------------------------*/

    // References to buttons
    [SerializeField] private GameObject[] buttons;

    // Reference to AITrafficPool
    [SerializeField] private AITrafficPool _AITrafficPool;

    // References to flag
    [SerializeField] private GameObject flag;

    // Reference to Elevator Animator
    [SerializeField] private Animator animator;

    // References to istantiated levers 
    private GameObject currentLevers;
    
    // References to flag
    private GameObject currentFlag;
    private float flagSpeed;

    // To change hinge joint limits
    private float hingeMax;

    // To change position flag
    private Vector3 newPositionFlag;

    /*----------------INITIALIZATION METHODS-----------------------------*/

    // The singleton allows it to be used directly from outside using ElevatorManager.instance I can use all the public methods that belong to it.
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        numberButtonPressed = 0;
        buttons[numberButtonPressed].GetComponent<HandTouchEvent>().enabled = false;
        buttons[numberButtonPressed].GetComponent<ButtonScript>().Activate();
    }

    /*----------------PUBLIC METHODS-----------------------------*/

    // Change image button to activated and block touch button and change levers
    public void ActivateButton(int nbp)
    {
        ElevatorManager.Instance.used = true;
        numberButtonPressed = nbp;
        DisableButtons(numberButtonPressed);

        // Destroy current levers
        Destroy(currentLevers);
        Destroy(currentFlag);

        // Wait 2.5s
        StartCoroutine(WaitAndExecute());
    } 

    // Enable buttons
    public void EnableButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<HandTouchEvent>().enabled = true;
        }
    }

    /*----------------PRIVATE METHODS-----------------------------*/

    // Disable buttons
    private void DisableButtons(int nbp)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != nbp)
            {
                buttons[i].GetComponent<ButtonScript>().Deactivate();
                buttons[i].GetComponent<HandTouchEvent>().enabled = false;
            }
            else
            {
                buttons[i].GetComponent<HandTouchEvent>().enabled = false;
            }
         
    }
    }

    private IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(2.5f);
        // The code following this instruction will be executed after 2.5 seconds. It is used to wait for the door to close.
        // Activates the lever in the necessary floor and start elevator animation, it also set the hingemax for max opacity.
        newRotationLevers = Quaternion.Euler(new Vector3(0, 271.3117f, 0));

        // I manage which values to set for each floor
        ExecuteButtonAction();

        // Instantiate current levers
        currentLevers = Instantiate(levers, newPositionLevers, newRotationLevers);
        if (numberButtonPressed > 0)
        {
            currentFlag = Instantiate(flag, newPositionFlag, Quaternion.Euler(new Vector3(0, 0, 0)));

            // Set the flag speed movement
            currentFlag.transform.GetChild(0).GetComponent<Cloth>().randomAcceleration = new Vector3(flagSpeed, flagSpeed, flagSpeed);
        }

        // Set lever max rotations
        HingeJoint hingeFloor = currentLevers.transform.GetChild(0).GetChild(0).gameObject.GetComponent<HingeJoint>();
        HingeJoint hingeRailings = currentLevers.transform.GetChild(1).GetChild(0).gameObject.GetComponent<HingeJoint>();
        JointLimits limitsFloor = hingeFloor.limits;
        JointLimits limitsRailings = hingeRailings.limits;
        limitsRailings.max = hingeMax;
        limitsFloor.max = hingeMax;
        hingeFloor.limits = limitsFloor;
        hingeRailings.limits = limitsRailings;

        // Set the rotation of the levers to the minimum value
        currentLevers.transform.GetChild(0).GetChild(0).Rotate(50, 0, 0, Space.Self);
        currentLevers.transform.GetChild(1).GetChild(0).Rotate(50, 0, 0, Space.Self);



    }

    private void ExecuteButtonAction()
    {
        switch (numberButtonPressed)
        {
            case 0:
                animator.SetInteger("Target Floor", 0);
                hingeMax = 45;
                newPositionLevers = new Vector3(717.3992f, 23.92498f, -1184.438f);
                newRotationLevers = Quaternion.Euler(new Vector3(0, 132.4561f, 0));
                _AITrafficPool.spawnZone = 300;
                break;
            case 1:
                animator.SetInteger("Target Floor", 1);
                hingeMax = 45;
                newPositionLevers = new Vector3(666.515f, 69.57303f, -1111.941f);
                newPositionFlag = new Vector3(665.202f, 68.294f, -1108.861f);
                flagSpeed = 5;
                _AITrafficPool.spawnZone = 350;
                break;
            case 2:
                animator.SetInteger("Target Floor", 2);
                hingeMax = 36;
                newPositionLevers = new Vector3(666.515f, 115.101f, -1111.941f);
                newPositionFlag = new Vector3(665.202f, 113.815f, -1108.861f);
                flagSpeed = 10;
                _AITrafficPool.spawnZone = 350;
                break;
            case 3:
                animator.SetInteger("Target Floor", 3);
                hingeMax = 29;
                newPositionLevers = new Vector3(666.515f, 160.883f, -1111.941f);
                newPositionFlag = new Vector3(665.202f, 159.606f, -1108.861f);
                flagSpeed = 20;
                _AITrafficPool.spawnZone = 360;
                break;
            case 4:
                animator.SetInteger("Target Floor", 4);
                hingeMax = 21.5f;
                newPositionLevers = new Vector3(666.515f, 206.736f, -1111.941f);
                newPositionFlag = new Vector3(665.202f, 205.45f, -1108.861f);
                flagSpeed = 30;
                _AITrafficPool.spawnZone = 365;
                break;
            case 5:
                animator.SetInteger("Target Floor", 5);
                hingeMax = 15.3f;
                newPositionLevers = new Vector3(666.515f, 252.07f, -1111.941f);
                newPositionFlag = new Vector3(665.202f, 250.784f, -1108.861f);
                flagSpeed = 40;
                _AITrafficPool.spawnZone = 385;
                break;
            case 6:
                animator.SetInteger("Target Floor", 6);
                hingeMax = 11.5f;
                newPositionLevers = new Vector3(666.515f, 297.458f, -1111.941f);
                newPositionFlag = new Vector3(666.038f, 296.119f, -1107.551f);
                flagSpeed = 50;
                _AITrafficPool.spawnZone = 415;
                break;
            default:
                break;
        }
    }

    
}
