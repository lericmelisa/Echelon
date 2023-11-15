using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
   public GameObject efekt;
   private void OnCollisionEnter(Collision collision)
   {
      
      //JUST DONT FORGET TO PUT TAG ON OBJECT WE WANT DO DESTROZ AND COLLIDER ALSO
      if (collision.gameObject.CompareTag("Bullet"))
      {
         Instantiate(efekt, gameObject.transform.position+new Vector3(25f,0f,0f), Quaternion.identity);
         Destroy(gameObject);

      }
   }
}
