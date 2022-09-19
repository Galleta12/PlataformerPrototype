using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : PlayerBaseState
{
    
    
    private readonly int NormalStateBlendHash = Animator.StringToHash("NormalStateBlend"); 
    private readonly int NormalBlendSpeedHash = Animator.StringToHash("NormalBlendSpeed"); 

    private const float AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;
    
    
    public PlayerNormalState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
        stateMachine.InputReader.JumpEvent += OnJump;
         stateMachine.InputReader.DashEvent += OnDash;
        stateMachine.Animator.CrossFadeInFixedTime(NormalStateBlendHash, CrossFadeDuration);
    }

    
     public override void Tick(float deltaTime)
    {
        // this handles the movement
        // Debug.Log(stateMachine.ForceReceiver.gravity);
        // Debug.Log(stateMachine.ForceReceiver.intialJumpVelocity);
        // Debug.Log(stateMachine.ForceReceiver.verticalVelocity);
        
        stateMachine.movement_input = CalculateMovement();

        Move(stateMachine.movement_input * stateMachine.FreeLookMovementSpeed, deltaTime);
        

       if(stateMachine.InputReader.MovementValue == Vector2.zero){
        // if the movement value is 0
        // set the blend tree to 0, idle
        stateMachine.Animator.SetFloat(NormalBlendSpeedHash, 0 , AnimatorDampTime, deltaTime);
        return;
        
       }
       else if(stateMachine.InputReader.IsAttacking){
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0,LastLook()));
        return;
       }
       else if(!stateMachine.Controller.isGrounded){
        stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        return;
       }
        
        
        // pass to the move method on the base state, the vector direction with the speed
      
        // set the blend tree to 0, run
        stateMachine.Animator.SetFloat(NormalBlendSpeedHash, 1 , AnimatorDampTime, deltaTime);

       FaceLook(stateMachine.movement_input, deltaTime);
    }

  
    public override void Exit()
    {
       stateMachine.InputReader.JumpEvent -= OnJump;
       stateMachine.InputReader.DashEvent -= OnDash;
     
    }

  
   
   

   
}
