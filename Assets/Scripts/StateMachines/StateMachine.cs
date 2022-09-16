using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    
    // current state of the state machine
    private State currentState;
    
    public void SwitchState(State newState){
      // Getting out of the current state always check if it is null
      currentState?.Exit();
      // change the state with the new state
      currentState = newState;
      // enter into the new state
      currentState?.Enter();

      
    }

    public State checkState(){
          
          return currentState;
    }

    
    // Update is called once per frame
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
  
}
