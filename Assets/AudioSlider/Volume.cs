using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class Volume : MonoBehaviour
{
   //references on volumeslider  and audiomixer

   public Slider volumeSlider;
   public AudioMixer audioMixer;

   private void Start()
   {
      //assignin previous value to audio mixer and volume slider
      volumeSlider.value= PlayerPrefs.GetFloat("volume");

     
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
      audioMixer.SetFloat("volume", volumeSlider.value);
   
      //saving that value into this f
      PlayerPrefs.SetFloat("volume",volumeSlider.value);

   }

   
}
