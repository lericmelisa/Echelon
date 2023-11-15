using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabledDisabledGlavna : MonoBehaviour
{
    // Start is called before the first frame update

    
    
    public GameObject objectToEnable1;
    
    public GameObject objectToDestroy1;
 
    public Renderer objectRenderer;
    private float timer;
    private float pozicija;
    private void Start()
    {
// Disable the renderer
if(objectRenderer!=null)
        objectRenderer.enabled = false;
    }
    
   
  

        private void OnTriggerEnter(Collider collider)
        {

            if (collider.CompareTag("Player"))
            {
                
                if(objectToEnable1!=null)
                    objectToEnable1.SetActive(true);
                Destroy(gameObject);
                if(objectToDestroy1!=null)
                    objectToDestroy1.SetActive(false);
                
                    
            }
        }
}



    
    
