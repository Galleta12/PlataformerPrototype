using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
       this.stateMachine = stateMachine;
    }


  protected void Move(Vector3 direction, float deltaTime){

    stateMachine.Controller.Move((direction + stateMachine.forceReceiverEnemy.Movement) * deltaTime);
     
  }


  


}
