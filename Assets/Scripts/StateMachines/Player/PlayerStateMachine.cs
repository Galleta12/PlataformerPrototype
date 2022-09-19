using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
// this serialize the field on the ispector, this is for storing the object states into a format that Unitu can store and use later
[field: SerializeField] public InputReader InputReader {get; private set; }

[field: SerializeField] public CharacterController Controller {get; private set; }


[field: SerializeField] public Animator Animator {get; private set; }

[field: SerializeField] public ForceReceiver ForceReceiver {get; private set; }



[field: SerializeField] public float FreeLookMovementSpeed {get; private set; }

[field: SerializeField] public float JumpMoveSpeed {get; private set; }





[field: SerializeField] public float RotationDampSpeed {get; private set; }
[field: SerializeField] public float DashTime {get; private set;}

[field: SerializeField]public float DashForce {get; private set;}

public Transform MainCameraPlayer {get; private set; }

[field: SerializeField] public AttackData [] Attacks {get; private set;}

[HideInInspector]
public Vector3 movement_input;



private void Start(){
   
    MainCameraPlayer = Camera.main.transform;
    
    if(!Controller.isGrounded){
      SwitchState(new PlayerFallingState(this));
      return;
    }
     SwitchState(new PlayerNormalState(this));
    
}


// public void Update(){
//     Debug.Log("Hello");
// }


// private void OnControllerColliderHit(ControllerColliderHit hit) {

//         if(!Controller.isGrounded && hit.normal.y < 0.1f){
           
//            Debug.DrawRay(hit.point,hit.normal, Color.red,1.25f);
          
//             Debug.Log(hit.normal);
//                SwitchState(new PlayerWallJumpState(this, hit));
//            }
       
        
//     }

        
        
        
        
  


    


}
