using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    
    private float remainingDashTime;
    private Vector3 dashInputs;

    private readonly int DashGroundTreeHash = Animator.StringToHash("DashGroundBlend");
    private readonly int DashSpeedHash = Animator.StringToHash("DashGroundSpeed");

    private readonly int DashJumpHash = Animator.StringToHash("DashJump");
    

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    
    public PlayerDashState(PlayerStateMachine stateMachine, Vector3 dashInputs) : base(stateMachine)
    {
        
       if(dashInputs == Vector3.zero){
        this.dashInputs = DashZeroLook();
       
       }else if(dashInputs != Vector3.zero){
            this.dashInputs = dashInputs;
       }
        
    }

    public override void Enter()
    { 
          remainingDashTime = stateMachine.DashTime;

       UpdateAnimator(Time.deltaTime);
       
       
      

        
    }


      public override void Tick(float deltaTime)
    {
       
        if(!stateMachine.Controller.isGrounded){
            MoveDashAir(dashInputs * stateMachine.DashForce  / stateMachine.DashTime, deltaTime);
            
        }else if(stateMachine.Controller.isGrounded){
          
        Move(dashInputs * stateMachine.DashForce  / stateMachine.DashTime, deltaTime);
        }
         
        FaceLook(dashInputs, deltaTime);


        remainingDashTime-= deltaTime;
        
        
        
        
        if (remainingDashTime <= 0f)
        {
            
            if(!stateMachine.Controller.isGrounded){
              stateMachine.SwitchState(new PlayerFallingState(stateMachine));
              return;
            }

              stateMachine.SwitchState(new PlayerNormalState(stateMachine));
        }


    }

    public override void Exit()
    {
        
    }


    private void UpdateAnimator(float deltaTime)
    {
       float valuey = dashInputs.y;
       float valuex = dashInputs.x;
       
         if(!stateMachine.Controller.isGrounded){
           stateMachine.Animator.CrossFadeInFixedTime(DashJumpHash,CrossFadeDuration);
         }if(stateMachine.Controller.isGrounded){
                 stateMachine.Animator.CrossFadeInFixedTime(DashGroundTreeHash, CrossFadeDuration);
                 stateMachine.Animator.SetFloat(DashSpeedHash, 0 , AnimatorDampTime, deltaTime);
                 
         }
   

        
    }

    

  
}

