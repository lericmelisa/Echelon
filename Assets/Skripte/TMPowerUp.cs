using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Movement;

public class TMPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Throwing tpScript;

    public TimeManipulation tm;

    public SwingingDone swinging;
    public GameObject krug;
    public Material[] materials;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (swinging.GetComponent<SwingingDone>().enabled == true)
                swinging.GetComponent<SwingingDone>().enabled = false;
            if (tpScript.GetComponent<Throwing>().enabled == true)
                tpScript.GetComponent<Throwing>().enabled = false;
            Renderer krugRenderer = krug.GetComponent<Renderer>();
            krugRenderer.sharedMaterials = materials;
            tm.GetComponent<TimeManipulation>().enabled = true;
            Destroy(gameObject);

        }
    }
    
}