using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisabledOnBullet : MonoBehaviour
{
    // Start is called before the first frame update


    
    public GameObject objectToEnable1;
    
    public GameObject objectToDestroy1;
 
    public Renderer objectRenderer;
    private void Start()
    {
     

// Disable the renderer
        objectRenderer.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.CompareTag("Bullet"))
        {
            objectToEnable1.SetActive(true);
            Destroy(gameObject);
            Destroy(objectToDestroy1);
            
        }
    }
}