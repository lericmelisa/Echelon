using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnOFFCanvas : MonoBehaviour
{
    public float timerDuration = 5f;
    private float timer;
    private bool canPressKey = false;
    public Canvas canvasToDisable;
    public Canvas canvasToEnable;
    [SerializeField] TMP_Text tmpProText;

   
    private void Start()
    {
        timer = timerDuration;
       
    }

    private void Update()
    {
        if (!canPressKey)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                canPressKey = true;
                Debug.Log("You can now press any key!");
            }
        }

        if (canPressKey && Input.anyKeyDown)
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);
                // Disable the current canvas
                canvasToDisable.enabled = false;

                // Enable the target canvas
                canvasToEnable.enabled = true;
                tmpProText.enabled = true;

            }
        }
    }
}
