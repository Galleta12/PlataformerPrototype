using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiverEnemy : MonoBehaviour
{
   
   [SerializeField] private CharacterController controller;

    [SerializeField] private float drag = 0.1f;

   
    private float verticalVelocity;

    private Vector3 dampingVelocity;

   
    
    public Vector3 Movement =>  impact +  (Vector3.up * verticalVelocity);

    

    private Vector3 impact;



    private float gravity = -9.8f;

   private float groundGravity = -.05f;


    
    private float intialJumpVelocity;

  

    private float maxJumpHeight = 1.0f;

    private float maxJumpTime = 0.5f;

    private bool isSlope = false;


 

    

   
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
        // reduce impact until is 0
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

   
     }

     public void AddForce(Vector3 force){
       impact += force;

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



    

    

       


}
