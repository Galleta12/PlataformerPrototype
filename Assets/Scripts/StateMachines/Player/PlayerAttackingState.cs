using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    
   
    private AttackData attack;
   
   private Vector3 currentMove;

   private Vector3 lastLookCamera;
    
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attckIndx, Vector3 lastlook) : base(stateMachine)
    {
        this.attack = stateMachine.Attacks[attckIndx];
        this.lastLookCamera = lastlook;
    }  

    public override void Enter()
    {
       stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
       

      
    }
    public override void Tick(float deltaTime)
    {

        Move(Vector3.zero,deltaTime);
        
        
        FaceLookAttack(deltaTime);
        
        
        

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if(normalizedTime < 1f){
             if(stateMachine.InputReader.IsAttacking){
            ComboAttack(normalizedTime);
            this.lastLookCamera = LastLook();
        }
        }
        else{
      
             stateMachine.SwitchState(new PlayerNormalState(stateMachine));
      
       }
        
      
  
    }

   

    public override void Exit()
    {
       
    }

     private void ComboAttack(float normalizedTime)
    {
    //recursion is the index of the current animation
      if(attack.ComboStateIndex == -1){return;}
        if(normalizedTime < attack.ComboAttackTime){ return;}

        stateMachine.SwitchState(
            new PlayerAttackingState(
                stateMachine,
                attack.ComboStateIndex,
                 this.lastLookCamera
            )
        );

    }


       private void FaceLookAttack(float deltaTime)
    {
      
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, 
                Quaternion.LookRotation(this.lastLookCamera), 
                deltaTime * stateMachine.RotationDampSpeed);
    }


        
    
}
