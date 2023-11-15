using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject objectToEnable1;
    
    public GameObject objectToDestroy1;
    private void OnCollisionEnter(Collision collision)
    {
      
        //JUST DONT FORGET TO PUT TAG ON OBJECT WE WANT DO DESTROZ AND COLLIDER ALSO
        if (collision.gameObject.CompareTag("Bullet"))
        {
            objectToEnable1.SetActive(true);
            Destroy(objectToDestroy1);

        }
    }
}
