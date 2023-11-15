using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EscapeMenu : MonoBehaviour, IDataPersistance
{
    public static bool isPaused;

    public bool presao;

    public Camera rukaCam;

    public GameObject crosshair;

    public GameObject timer;

    public GameObject meni;

    public GameObject crnoBijelo;

    public bool promjena = false;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    public void LoadData(GameData data, BatteryData data2)
    {
        presao = data.levelPassed;
    }

    public void SaveData(ref GameData data, ref BatteryData data2)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            isPaused = true;
            rukaCam.enabled = false;
            crosshair.SetActive(false);
            if(timer!=null)
                timer.SetActive(false);
            if (crnoBijelo != null)
            {
                if (crnoBijelo.activeSelf)
                {
                    crnoBijelo.SetActive(false);
                    promjena = true;
                }

            }
            meni.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            UnPauseGame();
        }
    }

    public void UnPauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isPaused = false;
        rukaCam.enabled = true;
        crosshair.SetActive(true);
        if(timer!=null)
            timer.SetActive(true);
        if (crnoBijelo != null)
        {
            if(promjena)
                crnoBijelo.SetActive(true);
        }
        meni.SetActive(false);
    }
}
