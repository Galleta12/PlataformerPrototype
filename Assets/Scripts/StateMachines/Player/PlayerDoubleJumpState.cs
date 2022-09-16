using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerBaseState
{
    
    private readonly int BackFlipHash = Animator.StringToHash("BackFlip");

    private const float CrossFadeDuration = 0.1f;
    
    public PlayerDoubleJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BackFlipHash,CrossFadeDuration);
        stateMachine.ForceReceiver.DoubleJump();
         stateMachine.InputReader.DashEvent += OnDash;
        
    }

    
    public override void Tick(float deltaTime)
    {
       
       
       Move(stateMachine.movement_input, deltaTime);


       if (stateMachine.Controller.velocity.y <= -10.0f)
        {
           
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }else if(!stateMachine.Controller.isGrounded){
            
            
            stateMachine.movement_input= CalculateMovement();
            
            Move(stateMachine.movement_input * stateMachine.JumpMoveSpeed, deltaTime);
            FaceLook(stateMachine.movement_input,deltaTime);
            return;
           
        }else if(stateMachine.Controller.isGrounded){
            stateMachine.SwitchState(new PlayerNormalState(stateMachine));
        }
    }


    public override void Exit()
    {
        stateMachine.InputReader.DashEvent -= OnDash;
    }

}