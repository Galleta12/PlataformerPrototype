using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerBaseState : State
{
  
   
   
   protected PlayerStateMachine stateMachine;

   
   


    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
       this.stateMachine = stateMachine;
    }
    
   


   protected void Move(Vector3 motion, float deltaTime){
    
    
    stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);

    
   }


    protected void MoveDashAir(Vector3 motion, float deltaTime){
    
    
    stateMachine.Controller.Move(motion * deltaTime);

    
   }



    protected Vector3 CalculateMovement(){
       // z and x axis of the camera we get the vectors of it
       Vector3 camera_z = stateMachine.MainCameraPlayer.forward;
       Vector3 camera_x = stateMachine.MainCameraPlayer.right;
        // we dont care about the y axis of the camera
       camera_z.y = 0f;
       camera_x.y = 0f;
       
       camera_z.Normalize();
       camera_x.Normalize();

       
    //    float xx = stateMachine.InputReader.MovementValue.x;
    //    float yy = stateMachine.InputReader.MovementValue.y;
    //    Vector3 direction = new Vector3(xx, 0f, yy).normalized;
       return stateMachine.InputReader.MovementValue.x * camera_x + 
       stateMachine.InputReader.MovementValue.y * camera_z;
    }


    protected Vector3 DashZeroLook(){
      
       Vector3 camera_z = stateMachine.MainCameraPlayer.forward;
       Vector3 camera_x = stateMachine.MainCameraPlayer.right;
       camera_z.y = 0f;
       camera_x.y = 0f;
       
       camera_z.Normalize();
       camera_x.Normalize();

       return camera_z;
    
    }



        protected Vector3 LastLook(){
      
       Vector3 camera_z = stateMachine.MainCameraPlayer.forward;
       Vector3 camera_x = stateMachine.MainCameraPlayer.right;
       camera_z.y = 0f;
       camera_x.y = 0f;
       
       camera_z.Normalize();
       camera_x.Normalize();

       return camera_z;
    
    }


      protected void FaceLook(Vector3 movement_input, float deltaTime)
    {
        // turn around using quaternion . lerp
        if(movement_input == Vector3.zero){return;}
        
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation, 
                Quaternion.LookRotation(movement_input), 
                deltaTime * stateMachine.RotationDampSpeed);
    }



   

    
   protected void OnJump()
    {
       if(stateMachine.Controller.isGrounded){
           stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
       }else{}
        
            
       
    }

     protected void OnDash()
    {
       stateMachine.SwitchState(new PlayerDashState(stateMachine, stateMachine.movement_input));      
       
    }



   


   


   

    
}
