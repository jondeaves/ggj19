    using UnityEngine;
    using System.Collections;
    
    public class MoveToOld : MonoBehaviour
    {
       public Transform goal;
       
       void Start ()
       {
          UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
          agent.destination = goal.position; 
       }
    }