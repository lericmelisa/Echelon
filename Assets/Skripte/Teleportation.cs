using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    
    public Transform PlayerObj;
    public CustomBullet cb;
    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Vector3 newPosition = new Vector3(3, 1, 3);
        //    transform.position = newPosition;
        //}
        //if(cb.tp == true)
        //    Teleport();
    }
    public void Teleport()
    {
       //PlayerObj.position = cb.TpPosition;
       //Debug.Log("Teleport position " + cb.TpPosition);
       //cb.tp = false;
    }
}
