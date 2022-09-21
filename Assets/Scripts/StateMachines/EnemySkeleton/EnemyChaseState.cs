using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{

     private readonly int NormalStateBlendHash = Animator.StringToHash("EnemyBlendTree"); 
    private readonly int NormalBlendSpeedHash = Animator.StringToHash("SpeedNormalBlend"); 

     private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    private Transform closestTarget; 

    private float previousviewradious;
    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
      
      
          stateMachine.Animator.CrossFadeInFixedTime(NormalStateBlendHash, CrossFadeDuration);
      
      
      Transform checkCloser = stateMachine.FieldOfView.visibleTargets[0];
      Vector3 distance =  stateMachine.FieldOfView.visibleTargets[0].position - stateMachine.transform.position ;
      previousviewradious = stateMachine.FieldOfView.viewRadius;
      stateMachine.FieldOfView.viewRadius *= 2f;
      
       // to check
       for (int i=0; i < stateMachine.FieldOfView.visibleTargets.Count; i ++){
         
          Vector3 currentdistance =  stateMachine.FieldOfView.visibleTargets[i].position - stateMachine.transform.position ;
          
          if (distance.sqrMagnitude >= currentdistance.sqrMagnitude){
             checkCloser = stateMachine.FieldOfView.visibleTargets[i];
          }
         Debug.Log(stateMachine.FieldOfView.visibleTargets[i].tag);

       }
       
       closestTarget = checkCloser;
    }

    

    public override void Tick(float deltaTime)
    {
   
   
   stateMachine.Animator.SetFloat(NormalBlendSpeedHash, 1 , AnimatorDampTime, deltaTime);
   Debug.Log("This is the closer" + closestTarget.gameObject.tag);
       chaseTheClose(deltaTime);
       LookTarget(closestTarget);
       chasecurrent();
       
        
    }


    public override void Exit()
    {
      stateMachine.FieldOfView.viewRadius = previousviewradious;
    }

    private void chaseTheClose(float deltaTime){

      
      
      if(stateMachine.Agent.isOnNavMesh){
    stateMachine.Agent.destination = closestTarget.position;

     Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed,deltaTime);

      }
      stateMachine.Agent.velocity = stateMachine.Controller.velocity;
   
    }




     private void LookTarget(Transform currentTarget){
        if (currentTarget == null) {return;}
        Vector3 lookpos = currentTarget.position - stateMachine.transform.position;
        lookpos.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookpos);

    }


    private void chasecurrent(){
        Collider[] currenttargetsInViewRadius = Physics.OverlapSphere (stateMachine.transform.position, stateMachine.FieldOfView.viewRadius, stateMachine.FieldOfView.targetMask);
        bool checker = false;
        for(int i = 0; i < currenttargetsInViewRadius.Length; i++){
           if(currenttargetsInViewRadius[i].gameObject.tag == closestTarget.gameObject.tag){
            checker = true;
           }
        //    Debug.Log(currenttargetsInViewRadius[i]);
        //     Debug.Log(closestTarget);
        }
        
        if(checker == true){
            Debug.Log("True");
        }else{
            stateMachine.SwitchState(new EnemyWayPointState(stateMachine));
        }
    }
}
