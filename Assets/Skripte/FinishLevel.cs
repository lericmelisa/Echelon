using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevel : MonoBehaviour, IDataPersistance
{
    public Slider timerSlider;
    
    public float vrijeme;
    public static bool prosao;
    public bool presao;
    public static bool passed;
    public DataPersistanceManager dpm;
    public GameObject meniPresao;
    public GameObject meniFail;
    public GameObject meniTutorial;
    public Camera rukaCam;
    public GameObject crosshair;
    public GameObject timer;
    public GameObject crnoBijelo;
    public bool promjena = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.GetFloat("Time", 1);
        Debug.Log("BrojLevela" + PlayerPrefs.GetInt("levelsUnlocked"));
        presao = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        vrijeme += Time.deltaTime;
    }

    public void LoadData(GameData data, BatteryData data2)
    {
        passed = data.levelPassed;
    }
    public void SaveData(ref GameData data, ref BatteryData data2)
    {
        if(data.finalVrijeme>this.vrijeme)
            data.finalVrijeme = this.vrijeme;
        if (!data.levelPassed && presao)
        {
            data.levelPassed = this.presao;
            int currentLvl = SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt("levelsUnlocked",currentLvl);
        }

    }
    


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Ruka"))
        {
            prosao = true;
            if (timer != null)
            {
                
                if (vrijeme < timerSlider.maxValue || passed)
                {
                    if (meniPresao != null)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Time.timeScale = 0f;
                        rukaCam.enabled = false;
                        crosshair.SetActive(false);
                        if(timer!=null)
                            timer.SetActive(false);
                        if (crnoBijelo != null)
                        {
                            if (crnoBijelo.activeInHierarchy)
                            {
                                crnoBijelo.SetActive(false);
                                promjena = true;
                            }

                        }
                        meniPresao.SetActive(true);
                        EscapeMenu.isPaused = true;
                        presao = true;
                    }
                    
                }
                else
                {
                    if (meniFail != null)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Time.timeScale = 0f;
                        rukaCam.enabled = false;
                        crosshair.SetActive(false);
                        if(timer!=null)
                            timer.SetActive(false);
                        if (crnoBijelo != null)
                        {
                            if (crnoBijelo.activeInHierarchy)
                            {
                                crnoBijelo.SetActive(false);
                                promjena = true;
                            }

                        }
                        meniFail.SetActive(true);
                        EscapeMenu.isPaused = true;
                        presao = false;
                    }

                }
            }
            else
            {
                if (meniTutorial != null)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0f;
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
                    meniTutorial.SetActive(true);
                    EscapeMenu.isPaused = true;
                    presao = true;
                }
            }
            Debug.Log("Presao: "+presao);
            dpm.SaveGame();
        }
    }
}
