using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
  public abstract void Enter();
  public abstract void Tick(float deltaTime);

  public abstract void Exit();

protected float GetNormalizedTime(Animator animator){

  AnimatorStateInfo currentAnimator = animator.GetCurrentAnimatorStateInfo(0);
  AnimatorStateInfo nextAnimator = animator.GetNextAnimatorStateInfo(0);


    if(animator.IsInTransition(0) && nextAnimator.IsTag("Attack")){
            return nextAnimator.normalizedTime;
        }else if(!animator.IsInTransition(0) && currentAnimator.IsTag("Attack")){
            return currentAnimator.normalizedTime;
        }else{
            return 0f;
        }





}






}
