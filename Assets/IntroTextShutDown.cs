using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTextShutDown : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName;
    public float delayTime = 12.0f;

    private bool canLoadScene = false;

    private void Start()
    {
        // Start the coroutine to enable scene loading after the delay time
        StartCoroutine(EnableSceneLoad());
    }
    

    private void Update()
    {
        // Check if any key is pressed and scene loading is enabled
        if (canLoadScene && Input.anyKeyDown)
        {
            // Load the next scene
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator EnableSceneLoad()
    {
        yield return new WaitForSeconds(delayTime);

        // Enable scene loading
        canLoadScene = true;
    }
}





