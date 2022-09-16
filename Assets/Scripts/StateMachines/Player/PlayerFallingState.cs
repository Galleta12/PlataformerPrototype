using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    
    private readonly int FallHash = Animator.StringToHash("FallingLand");


    private readonly int FallIdleHash = Animator.StringToHash("FallingIdle");

    private const float CrossFadeDuration = 0.1f;
    private Vector3 momentum;
    
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJumps;
        
        stateMachine.Animator.CrossFadeInFixedTime(FallIdleHash,CrossFadeDuration);

         stateMachine.InputReader.DashEvent += OnDash;
        

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.ForceReceiver.falling();
       Move(stateMachine.movement_input, deltaTime);
       
       
       

       

        if( stateMachine.Controller.isGrounded)
        {
            stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
            stateMachine.SwitchState(new PlayerNormalState(stateMachine));
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
       
       stateMachine.InputReader.JumpEvent -= OnJumps;
    }

    private void OnJumps()
    {
         stateMachine.InputReader.DashEvent -= OnDash;
        stateMachine.SwitchState(new PlayerDoubleJumpState(stateMachine));
    }

   


    
}
