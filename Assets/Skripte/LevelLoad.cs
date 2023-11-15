using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelLoad : MonoBehaviour
{
    public AudioMixer audioMusic;

    public AudioMixer audioEffects;
    private FileDataHandler dataHandler;
    //[SerializeField] private string fileName;
    //SerializeField] private bool useEncryption;
    // Start is called before the first frame update
    void Start()
    {
        audioMusic.SetFloat("Vol", Mathf.Log10(PlayerPrefs.GetFloat("volume")) * 20);
        audioEffects.SetFloat("Vol", Mathf.Log10(PlayerPrefs.GetFloat("volumeEffects")) * 20);
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        //this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
