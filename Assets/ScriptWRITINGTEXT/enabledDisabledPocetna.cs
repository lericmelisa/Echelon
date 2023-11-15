using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enabledDisabledPocetna : MonoBehaviour
{
    public float timerDuration = 5f;
    private float timer;
    private bool canPressKey = false;
    public GameObject canvasToDisable;
    public GameObject canvasToEnable;


    private bool hasBeenCalled = false;

    private void Start()
    {
        timer = timerDuration;
       
    }

    private void Update()
    {
        if (!canPressKey)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                canPressKey = true;
                Debug.Log("You can now press any key!");
            }
        }

        if (canPressKey && Input.anyKeyDown)
        {
            if (Input.anyKeyDown)
            {
            
                // Disable the current canvas
                canvasToDisable.SetActive(false);
                if (!hasBeenCalled)
                {
                    // Call the desired function or perform the action here

                    // Set the flag to true to ensure it doesn't get called again
                    canvasToEnable.SetActive(true);

                    hasBeenCalled = true;
                }

            }

          
        }
        
      
        
        
        
    }


  
}