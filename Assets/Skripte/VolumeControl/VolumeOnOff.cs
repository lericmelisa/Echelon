using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeOnOff : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Image img;

    public Sprite slikaOff;
    public Sprite slikaOn;
    public bool saved = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetVolume()
    {
        //name same as name of exposed parameter in audio mixer set through main mixer(audio mixer controller
        //righ click on volume exposed parameters....)


        //and call funtction on object volume slider we have list of functions
        //i attached script to canvas cause its created when we created volume slider

        //dont forget to add references on script on canvasvolumeslider!!!

        //when we put audio manager in levels we need to set mixer to master 

        //slider need to have min value -80 and max value 0 cause audio mixer max and mins
        if (PlayerPrefs.GetFloat("volume") != 0.001f)
        {
            //saved = true;
            //PlayerPrefs.SetFloat("Saved",PlayerPrefs.GetFloat("volume"));
            
            audioMixer.SetFloat("Vol", Mathf.Log10(0.001f)* 20);
            img.sprite = slikaOff;
            //saving that value into this f
            PlayerPrefs.SetFloat("volume",0.001f);
        }
        else
        {
            
            audioMixer.SetFloat("Vol",Mathf.Log10(0.5f)* 20);
            img.sprite = slikaOn;
            
            //saving that value into this f
            PlayerPrefs.SetFloat("volume",0.5f);
            
        }
        

    }
}
