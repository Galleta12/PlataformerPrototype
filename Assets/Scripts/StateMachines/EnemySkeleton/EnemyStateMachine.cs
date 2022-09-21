using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
   //[field: SerializeField] public Animator Animator {get; private set; }

   [field: SerializeField] public CharacterController Controller {get; private set; }

    [field: SerializeField] public NavMeshAgent Agent {get; private set; }

    [field: SerializeField] public Animator Animator {get; private set; }

    [field: SerializeField] public List<Transform> Waypoints {get; private set; }

   [field: SerializeField] public ForceReceiverEnemy forceReceiverEnemy {get; private set; }

    [field: SerializeField] public FieldOfView FieldOfView {get; private set; }



   [field: SerializeField] public float MovementSpeed {get; private set; }

   [field: SerializeField] public float RadiousView {get; private set; }


     [field: SerializeField] public float AngleView {get; private set; }

     
     [HideInInspector]
     public int targetWaypoint;

     



     


    private void Start() {
        
      
        Agent.updatePosition = false;
        Agent.updateRotation = false;
        SwitchState(new EnemyWayPointState(this));
     }



     private void OnTriggerEnter(Collider other) {
        
        
       
        
        int lenght = Waypoints.Count;
        //Debug.Log(hit.collider.gameObject.tag);
        
        if(other.gameObject.tag == "Waypoint"){
           // Debug.Log("True");
           targetWaypoint ++;
        
       
        
        if(targetWaypoint == lenght){
           Waypoints.Reverse();
            targetWaypoint = 1;
        }
        
        
        }else{
           // Debug.Log("NOthing");
        }
    }


    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.white;
      Gizmos.DrawWireSphere(transform.position,FieldOfView.viewRadius);
      Vector3 viewAngleA = FieldOfView.DirFromAngle(FieldOfView.viewAngle,false);
      Vector3 viewAngleB = FieldOfView.DirFromAngle(-FieldOfView.viewAngle,false);
      Gizmos.DrawLine(transform.position, transform.position + viewAngleA * FieldOfView.viewRadius);
    Gizmos.DrawLine(transform.position, transform.position + viewAngleB * FieldOfView.viewRadius);

    Gizmos.color = Color.green;

    foreach(Transform visibleTarget in FieldOfView.visibleTargets){
      Gizmos.DrawLine(transform.position, visibleTarget.position);
    }
     

    }


 



}
