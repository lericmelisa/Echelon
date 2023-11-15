using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MeniButtonFunk : MonoBehaviour
{

    private int minAllowedSceneIndex = 1; // Minimum allowed scene index
    private int maxAllowedSceneIndex = 6;
    public EscapeMenu esc;
    public EscMeniTutLevel escTut;
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject screenToHide;

    private static bool firstTime = false;
    private const string HasPlayedKey = "HasPlayed";
    private bool hasPlayed = false;
    public void LoadLevelAAAAAAAAAAAAAAA()
    {
        if (EscapeMenu.isPaused)
        {
            EscapeMenu.isPaused = false;
            Time.timeScale = 1f;

        }
        PlayerPrefs.SetInt("prviPut",1);

        if (!hasPlayed)
        {
            SceneManager.LoadScene(10);

        }
        else
        {
            SceneManager.LoadScene(1);

        }
        
    }

    public void Continue()
    {

        if (PlayerPrefs.GetInt("levelsUnlocked") == 0)
            return;
       else if (PlayerPrefs.GetInt("levelsUnlocked")+1<=maxAllowedSceneIndex)
        {
           
                StartCoroutine(LoadAsync(PlayerPrefs.GetInt("levelsUnlocked")+1));
        }
        else
        {
            StartCoroutine(LoadAsync(6));
        }
        




    }

    private void Awake()
    {

    }

    public void LoadLevelMenu1()
    {
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex); // Save the current scene name

        SceneManager.LoadScene(8);
        
    }
    public void LoadLevelMenu1FromLevelMenu2()
    {

        SceneManager.LoadScene(8);
        
    }
    public void LoadLevelMenu2()
    {
        //PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex); // Save the current scene name

        SceneManager.LoadScene(9);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

    }

  
    public void LoadOptionsMenu()
    {
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex); // Save the current scene name
        SceneManager.LoadScene(7); // Load the options menu scene
    }

    public void BackLastScene()
    {
        if (EscapeMenu.isPaused)
        {
            EscapeMenu.isPaused = false;
            Time.timeScale = 1f;

        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastScene")); // Load the last scene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void PreviousLevel()
    {
        if (EscapeMenu.isPaused)
        {
            EscapeMenu.isPaused = false;
            Time.timeScale = 1f;

        }
        if (SceneManager.GetActiveScene().buildIndex > minAllowedSceneIndex)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex-1));

        }
        
        
        
    }

    public void Resume()
    {
        if(esc!=null)
            esc.UnPauseGame();
        if(escTut!=null)
            escTut.UnPauseTutorial();
    }
    public void Play()
    {
        SceneManager.LoadScene(0);

    }
    
    public void NextLevel()
    {
       
        //if (Time.timeScale == 0f && FinishLevel.passed)
        //{
        //    EscapeMenu.isPaused = false;
        //    Time.timeScale = 1f;
//
        //}
        if (SceneManager.GetActiveScene().buildIndex + 1 <= maxAllowedSceneIndex &&
            SceneManager.GetActiveScene().buildIndex<=PlayerPrefs.GetInt("levelsUnlocked"))
        {

            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex+1));

        }
        
        
        
    }
    public void ReloadScene()
    {
        if (EscapeMenu.isPaused)
        {
            EscapeMenu.isPaused = false;
            Time.timeScale = 1f;

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   //public void LoadLvl(int indexLvl)
   //{
   //    if (EscapeMenu.isPaused)
   //    {
   //        EscapeMenu.isPaused = false;
   //        Time.timeScale = 1f;

   //    }
   //    StartCoroutine(LoadAsync(indexLvl + 1));
   //}

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        screenToHide.SetActive(false);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);
            slider.value = progress;
            yield return null;
        }
    }
    
    private void PlayOnce()
    {
        if (!hasPlayed)
        {
            SceneManager.LoadScene(10);

            hasPlayed = true; // Set the flag to true to indicate the function has been played

            SaveHasPlayedFlag(); // Save the flag value
        }
    }
    private void LoadHasPlayedFlag()
    {
        if (PlayerPrefs.HasKey(HasPlayedKey))
        {
            hasPlayed = PlayerPrefs.GetInt(HasPlayedKey) == 1;
        }
    }

    private void SaveHasPlayedFlag()
    {
        PlayerPrefs.SetInt(HasPlayedKey, hasPlayed ? 1 : 0);
        PlayerPrefs.Save();
    }
    public float delayTime = 12.0f;

    private bool canLoadScene = false;

    private void Start()
    {
        LoadHasPlayedFlag();
        // Start the coroutine to enable scene loading after the delay time
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            StartCoroutine(EnableSceneLoad());

        }
    }

    private void Update()
    {
        // Check if any key is pressed and scene loading is enabled
        if (canLoadScene && Input.anyKeyDown)
        {
            // Load the next scene
            StartCoroutine(LoadAsync(1));
        }
    }

    IEnumerator EnableSceneLoad()
    {
        yield return new WaitForSeconds(delayTime);

        // Enable scene loading
        canLoadScene = true;
    }
    
    
    
    
    
    
    
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop the game in the Unity Editor
#else
            Application.Quit(); // Quit the game in a built executable
#endif
    }
}
