using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private CharacterController controller; 

    public RaycastHit hit;


    private void Update() {
        

        Vector3 p1 = transform.position + controller.center;
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, controller.height / 2, transform.forward, out hit, 10))
        {
          if(hit.collider.gameObject.tag == "WallJump"){
            Debug.Log(hit.collider.gameObject);
          }
            
            distanceToObstacle = hit.distance;
        }
    }


    private void OnDrawGizmos() {
         RaycastHit hit;

        Vector3 p1 = transform.position + controller.center;
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, controller.height / 2, transform.forward, out hit, 10))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + controller.center, transform.forward * hit.distance);
            Gizmos.DrawWireSphere(transform.position + controller.center + transform.forward * hit.distance, transform.lossyScale.x/2);
            
            
            distanceToObstacle = hit.distance;
        }
    }
}
