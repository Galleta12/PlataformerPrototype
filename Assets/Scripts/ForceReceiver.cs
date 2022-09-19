using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
   
   [SerializeField] private CharacterController controller; 

   [field: SerializeField] private InputReader InputReader;


    private float verticalVelocity;

   
     // this will return a vector (0,vertical velovity,0)
    public Vector3 Movement =>  thisNormal + (Vector3.up * verticalVelocity);

    private Vector3 thisNormal;

    private Vector3 thisNormalDirection;

    private float gravity = -9.8f;

   private float groundGravity = -.05f;


    
    private float intialJumpVelocity;

  

    private float maxJumpHeight = 1.0f;

    private float maxJumpTime = 0.5f;

    private bool isSlope = false;


    public event Action WallJUMP;

    

   
   private void Start(){
    setupJumpVariable();
   }
  
   
   
   private void Update() {
        

           if(verticalVelocity < 0f && controller.isGrounded){
              if(isSlope == true){
                return;
              }
              
              verticalVelocity= groundGravity  * Time.deltaTime;
        }
        
        else{
                verticalVelocity += gravity * Time.deltaTime;
           
        }

   
     }



    private void setupJumpVariable()
    {
        float timeToApex =maxJumpTime / 2;
         gravity = (-2 * maxJumpHeight) / MathF.Pow(timeToApex, 2);
        intialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        // float secondJumpGravity = (-2 * (maxJumpHeight + 2)) / MathF.Pow((timeToApex * 1.25f), 2);
        // secondJumpVelocity = (2 * (maxJumpHeight + 2)) / (timeToApex * 1.25f);
    }


    public void Jump(){
        verticalVelocity = intialJumpVelocity;

    }

    public void falling(){
       float fallMultiplier = 2.0f;

       verticalVelocity += gravity *  fallMultiplier * Time.deltaTime;

    }

    public void DoubleJump(){
           
         
           
           verticalVelocity = intialJumpVelocity * 1.2f;
    }


    



    private void OnControllerColliderHit(ControllerColliderHit hit) {

        if(hit.gameObject.tag == "Slope"){
            
             isSlope = true;
        }else{
            isSlope= false;
        }

       thisNormal = Vector3.zero;

        if(!controller.isGrounded && hit.normal.y < 0.1f){
           
       
              //Debug.DrawRay(hit.point,hit.normal, Color.red,1.25f);
             
             verticalVelocity = intialJumpVelocity;
             thisNormal = hit.normal * 4.0f;
             thisNormalDirection = hit.normal;
             WallJUMP?.Invoke();
             transform.rotation =  Quaternion.LookRotation(hit.normal) ;
         
              
             
             
              
           }


        
    }



    
        
       


}
