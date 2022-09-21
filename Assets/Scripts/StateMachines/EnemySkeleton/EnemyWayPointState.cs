using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPointState : EnemyBaseState
{
    private readonly int NormalStateBlendHash = Animator.StringToHash("EnemyBlendTree"); 
    private readonly int NormalBlendSpeedHash = Animator.StringToHash("SpeedNormalBlend"); 

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;
    
    
    
    public EnemyWayPointState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
       // Debug.Log(this);
      
          stateMachine.Animator.CrossFadeInFixedTime(NormalStateBlendHash, CrossFadeDuration);
          
    }


      public override void Tick(float deltaTime)
    {

        Transform currentWaypoint = stateMachine.Waypoints[stateMachine.targetWaypoint];
        
        if(currentWaypoint == null){
             stateMachine.Animator.SetFloat(NormalBlendSpeedHash, 0 , AnimatorDampTime, deltaTime);
             return;
        }
        
        if( currentWaypoint != null){
          MoveTOwaypoint(currentWaypoint,deltaTime);

          LookWaypoint(currentWaypoint);
          stateMachine.Animator.SetFloat(NormalBlendSpeedHash, 1 , AnimatorDampTime, deltaTime);
           // stateMachine.Agent.SetDestination(stateMachine.Waypoints[targetWaypoint].position);
        
        }
       
    }

    public override void Exit()
    {
        stateMachine.Agent.velocity = Vector3.zero;
        stateMachine.Agent.SetDestination(stateMachine.transform.position);
        //stateMachine.Agent.ResetPath();
        
        //stateMachine.targetWaypoint = stateMachine.Waypoints.Count -1;
         
       
    }

    
    
    private void MoveTOwaypoint(Transform currentWaypoint,float deltaTime){
        
        
        if(stateMachine.Agent.isOnNavMesh){
             //stateMachine.Agent.SetDestination(stateMachine.Waypoints[targetWaypoint].position);
            

             
             stateMachine.Agent.destination = stateMachine.Waypoints[stateMachine.targetWaypoint].position;
             
           
      
                 Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed,deltaTime);
        }

         getTargets();
      

          stateMachine.Agent.velocity = stateMachine.Controller.velocity;
        
    }


    private void LookWaypoint(Transform currentWaypoint){
        if (currentWaypoint == null) {return;}
        Vector3 lookpos = currentWaypoint.position - stateMachine.transform.position;
        lookpos.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookpos);

    }


    private void getTargets(){
       if(stateMachine.FieldOfView.visibleTargets.Count > 0){
       stateMachine.SwitchState(new EnemyChaseState(stateMachine));


                         

       }
    }


private void OnWaypoint(){
    stateMachine.SwitchState(new EnemyChaseState(stateMachine));
  }
    
    
    

  
}
