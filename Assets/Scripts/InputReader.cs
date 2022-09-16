using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    
    //Vector will store the value movement of the input
    public Vector2 MovementValue {get; private set;}

   
  

     private Controls controls;


     public event Action JumpEvent;

     public event Action DashEvent;


     public bool  isJumping = false;
    
     private void Start()
    {
        // store instance of class controls
        controls = new Controls();
        // reference to this class
        controls.Player.SetCallbacks(this);
        // enable it
        controls.Player.Enable();
    }

    
    
    private void OnDestroy() {
        controls.Player.Disable();
    }
    
    
    public void OnLook(InputAction.CallbackContext context)
    {
       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
         
          isJumping = context.ReadValueAsButton();
           JumpEvent?.Invoke();
          
        
       
       
        
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(!context.performed){return ;}
        DashEvent?.Invoke();
    }
}
