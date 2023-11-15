using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //we take object we collided with ->player and we make it child of moving platform so we move it with it
            collision.gameObject.transform.SetParent(transform);
            // collision.gameObject.transform.SetParent setting parent of player to be moving platform-transform parts
        }
        
    }

     private void OnCollisionExit(Collision other)
     {
         if (other.gameObject.name == "Player")
         {
             //unparent it
             other.gameObject.transform.SetParent(null);
            
         }
         
     }
}
