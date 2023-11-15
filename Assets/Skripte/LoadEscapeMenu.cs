using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadEscapeMenu : MonoBehaviour, IDataPersistance
{
    public bool presao;
    public DataPersistanceManager dpm;

    public GameObject textToShow;

    public GameObject crosshair;

    public Camera rukaCam;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        

        Debug.Log("Ucitao podatak " + FinishLevel.passed);
        if (SceneManager.GetActiveScene().buildIndex == 1 && !FinishLevel.passed)
        {
            if (textToShow != null)
            {
                rukaCam.enabled = false;
                crosshair.SetActive(false);
                textToShow.SetActive(true);
            }
        }
    }
    public void LoadData(GameData data, BatteryData data2)
    {
        //this.presao = data.levelPassed;
    }

    public void SaveData(ref GameData data, ref BatteryData data2)
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (textToShow != null)
        {
            timer += Time.deltaTime;
            if (timer >= 15f)
            {
                rukaCam.enabled = true;
                crosshair.SetActive(true);
                textToShow.SetActive(false);
            }
        }

    }
}
