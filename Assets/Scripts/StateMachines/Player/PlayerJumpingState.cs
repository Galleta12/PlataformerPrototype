using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    
    
    
    
    private readonly int JumpHash = Animator.StringToHash("Jumping");

    private const float CrossFadeDuration = 0.1f;
    
    private Vector3  momentum;
    
    
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
         
       
       
      
        
       stateMachine.ForceReceiver.Jump();
        
     
     stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
      stateMachine.InputReader.DashEvent += OnDash;
        
        
    }

    

    public override void Tick(float deltaTime)
    {
      
       
        
        Move(stateMachine.movement_input, deltaTime);


       if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }else if(!stateMachine.Controller.isGrounded){
            
            
            
            
            
            stateMachine.movement_input = CalculateMovement();
            
            Move(stateMachine.movement_input * stateMachine.JumpMoveSpeed, deltaTime);
            FaceLook(stateMachine.movement_input,deltaTime);
            return;
           
        }

    }

    public override void Exit()
    {
         stateMachine.InputReader.DashEvent -= OnDash;
        
    }


   

   
  

    

 
}
