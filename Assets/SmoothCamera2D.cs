 using UnityEngine;
 using System.Collections;
 
 public class SmoothCamera2D : MonoBehaviour {
     
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;
     public Transform target;
     public Vector3 bottomLeft;
     public Vector3 topRight;
 
     // Update is called once per frame
     void FixedUpdate () 
     {
         if (target)
         {
             Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
             Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
             Vector3 destination = transform.position + delta;
             destination.x = Mathf.Min(Mathf.Max(destination.x,bottomLeft.x),topRight.x);
             destination.y = Mathf.Min(Mathf.Max(destination.y,bottomLeft.y),topRight.y);
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }
     
     }
 }