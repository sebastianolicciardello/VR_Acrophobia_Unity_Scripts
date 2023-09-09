using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathSpawner : MonoBehaviour
{

   // Constants
   private const string AGENT_PREFABS_FOLDER = "AgentPrefabs";

   /*----------------Serialized fields-----------------------------*/   
   
   // array of agent prefabs
   [SerializeField] private GameObject[] agentPrefabs; 

   // number of agents to spawn
   [SerializeField] private int numAgents = 10; 

   /*----------------INITIALIZATION METHODS-----------------------------*/

   void Awake()
   {
      InitializeAgentPrefabs();
   }

   private void InitializeAgentPrefabs()
   {
      // Load agent prefabs from "AgentPrefabs" folder in the Resources directory
      agentPrefabs = Resources.LoadAll<GameObject>(AGENT_PREFABS_FOLDER);
   }

   void Start() 
   {
      // get path points 
      Transform[] PathPoints = new Transform[transform.childCount];

      // loop through the path
      for (int i = 0; i < PathPoints.Length; i++) 
      {
         PathPoints[i] = transform.GetChild(i);
      }

      // spawn agents
      for (int i = 0; i < numAgents; i++)
      {

         // get a random index in the path
         int spawnIndex = Random.Range(0, PathPoints.Length); 

         // randomly determine if the path should be traversed in reverse
         bool reverse = Random.Range(0, 2) == 0; 

         // get a random agent prefab
         GameObject agentPrefab = agentPrefabs[Random.Range(0, agentPrefabs.Length)]; 

         // spawn the agent
         GameObject agentObject = Instantiate(agentPrefab, PathPoints[spawnIndex].position, Quaternion.identity);
         NavMeshAgent agent = agentObject.GetComponent<NavMeshAgent>();

         // check if the agent is null
         if (agentPrefab == null || agent == null)
         {
            Debug.LogError("Agent prefab or NavMeshAgent component is null.");
            continue;
         }
         
         // get a reference to the PeopleSystem component
         PeopleSystem peopleSystem = agent.GetComponent<PeopleSystem>(); 

         // check if the PeopleSystem component is null
         if (peopleSystem == null)
         {
            Debug.LogError("PeopleSystem component is null.");
            continue;
         }

         // set the agent's index to the spawn index
         peopleSystem.index = spawnIndex; 

         // set the agent's PATH to the path of reference
         peopleSystem.PATH = transform.gameObject; 

         // set the agent's reverse variable to the randomly determined value
         peopleSystem.reverse = reverse; 
      }
   }

}
