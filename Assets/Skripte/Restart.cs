using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject meniFail;
    public Camera rukaCam;

    public GameObject crosshair;

    public GameObject timer;
    public GameObject crnoBijelo;
    public Transform playerPos;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.y< -60f || Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        if (EscapeMenu.isPaused)
        {
            EscapeMenu.isPaused = false;
            Time.timeScale = 1f;

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            Debug.Log("Pogodjen player");

                Debug.Log("Pogodjen igrac");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                rukaCam.enabled = false;
                crosshair.SetActive(false);
                if(timer!=null)
                    timer.SetActive(false);
                if (crnoBijelo != null)
                {
                    crnoBijelo.SetActive(false);
                }
                meniFail.SetActive(true);
                EscapeMenu.isPaused = true;
        }
    }
}
