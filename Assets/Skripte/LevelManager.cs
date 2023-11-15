using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager: MonoBehaviour
{
        private int levelsUnlocked;
        public Button[] buttons;
        public Image[] images;
        public GameObject loadingScreen;
        public Slider slider;
        public GameObject screenToHide;
        private int minLevels = 1;
        private int maxLevels = 6;
        private void Start()
        {
                levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 0);
                if (buttons != null && images !=null)
                {
                        for (int i = 0; i < buttons.Length; i++)
                        {
                                buttons[i].interactable = false;
                                images[i].enabled = true;
                        }
                        for (int i = 0; i < levelsUnlocked+1; i++)
                        {
                                if (i <= maxLevels)
                                {
                                        buttons[i].interactable = true;
                                        images[i].enabled = false;    
                                }
                                else
                                        return;
                                


                        }
                }

        }

        public void LoadLvl(int indexLvl)
        {
                if (EscapeMenu.isPaused)
                {
                        EscapeMenu.isPaused = false;
                        Time.timeScale = 1f;

                }
                StartCoroutine(LoadAsync(indexLvl + 1));
        }

        IEnumerator LoadAsync(int sceneIndex)
        {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
                if (screenToHide!=null)
                {
                        screenToHide.SetActive(false);

                }

                loadingScreen.SetActive(true);

                while (!operation.isDone)
                {
                        float progress = Mathf.Clamp01(operation.progress / .9f);
                        Debug.Log(operation.progress);
                        slider.value = progress;
                        yield return null;
                }
        }
}