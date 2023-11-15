using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
     public void PlayGame()
        {
            //ucitat ce scenu iduceg levela po indexu na buld setting postavljaju se
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        public void QuitGame()
        {
            Debug.Log("quit");
            Application.Quit();
        }
        
        public void Back()
        {
            //ucitat ce scenu iduceg levela po indexu na buld setting postavljaju se
            //VALJDA UCITA PROSLU 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
}
