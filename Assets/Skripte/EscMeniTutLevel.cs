using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMeniTutLevel : MonoBehaviour
{

    public bool presao;

    public Camera rukaCam;

    public GameObject crosshair;

    public GameObject timer;

    public GameObject meni;

    public GameObject[] lista;
    public GameObject crnoBijelo;

    public bool promjena = false;
    // Start is called before the first frame update=  
    void Start()
    {
        EscapeMenu.isPaused = false;
    }

    public void LoadData(GameData data, BatteryData data2)
    {
        presao = data.levelPassed;
    }

    public void SaveData(ref GameData data, ref BatteryData data2)
    {
        
    }
    // Update is called once per frame
    private int pomocna = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && EscapeMenu.isPaused == false)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i] != null && lista[i].activeSelf)
                {
                    pomocna = i;
                    lista[i].SetActive(false);

                }
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            EscapeMenu.isPaused = true;
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
        else if(Input.GetKeyDown(KeyCode.Escape) && EscapeMenu.isPaused == true && !FinishLevel.prosao)
        {
            UnPauseTutorial();
        }
    }

    public void UnPauseTutorial()
    {
        lista[pomocna].SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        EscapeMenu.isPaused = false;
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
