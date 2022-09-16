using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    
    private readonly int WallTouchHash = Animator.StringToHash("TouchWall");

    private const float CrossFadeDuration = 0.1f;
    
    public PlayerWallJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(WallTouchHash, CrossFadeDuration);
    }
        


    public override void Tick(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
        if(stateMachine.Controller.isGrounded){
            stateMachine.SwitchState(new PlayerNormalState(stateMachine));
        }
    }

    public override void Exit()
    {
       
    }


    private void stopState(){
        stateMachine.SwitchState(new PlayerNormalState(stateMachine));
    }

    
}
