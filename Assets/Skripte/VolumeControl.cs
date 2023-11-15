using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;


    public void SetVolume()
    {
        audioMixer.SetFloat("Vol", Mathf.Log10(volumeSlider.value) * 20);
        PlayerPrefs.SetFloat("volume",volumeSlider.value);

    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("volume") == null)
        {
            volumeSlider.value = 0.5f;
        }
        else
        volumeSlider.value= PlayerPrefs.GetFloat("volume");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
