using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeopleSystem : MonoBehaviour
{
   /*----------------PUBLIC VARIABLES-----------------------------*/

   // reference to the path
   public GameObject PATH;   

   // index of the next point
   public int index;

   // whether to walk the path in reverse
   public bool reverse = false; 

   /*----------------PRIVATE VARIABLES-----------------------------*/

   // reference to the agent
   [SerializeField] private NavMeshAgent agent; 

   // reference to the animator
   [SerializeField] private Animator animator; 

   // reference to the path points
   private Transform[] PathPoints; 

   private float minDistance = 10f;

    /*----------------INITIALIZATION METHODS-----------------------------*/
   void Start() 
   {
      // get references
      agent = GetComponent<NavMeshAgent>();
      animator = GetComponent<Animator>();

      // Check if the agent reference is not null
        if (agent == null)
        {
             Debug.LogError("Agent reference is null. Make sure the NavMeshAgent component is attached to the GameObject.");
            return;
        }

      // Check if the animator reference is not null
        if (animator == null)
        {
            Debug.LogError("Animator reference is null. Make sure the Animator component is attached to the GameObject.");
            return;
        }

      // get path points 
      PathPoints = new Transform[PATH.transform.childCount];
      
      // loop through the path
      for (int i = 0; i < PathPoints.Length; i++) 
      {
         PathPoints[i] = PATH.transform.GetChild(i);
      }

      // set agent speed to half the original speed
      agent.speed = agent.speed / 5f;
   }

   void Update()
   {
       Roam();
   }

   /*----------------ROAM METHODS-----------------------------*/
   // The Roam function is responsible for controlling the movement of the agent along the defined path 
   void Roam()
    {
   try
   {
      // Check if the squared distance to the next point is less than the squared minimum distance
      if (Vector3.SqrMagnitude(transform.position - PathPoints[index].position) < minDistance * minDistance)
        {
            CalculateNextIndex();
        }

      // Set the agent's destination to the next point in the path
        agent.SetDestination(PathPoints[index].position);

      // Set the animator to walking if the agent is not stopped
      animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
   }
   catch (System.Exception e)
   {
      Debug.LogError("An error occurred while roaming: " + e.Message);
   }
    }

    private void CalculateNextIndex()
    {
        // Calculate the next index based on the "reverse" boolean
        int nextIndex = index + (reverse ? -1 : 1);

        // Check if the next index is valid
        if (nextIndex >= 0 && nextIndex < PathPoints.Length)
        {
            // Update the index
            index = nextIndex;
        }
        else
        {
            // Toggle the "reverse" boolean
            reverse = !reverse;

            // Update the index based on the "reverse" boolean
            index += reverse ? -1 : 1;
        }
    }
}
