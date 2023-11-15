using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAllText : MonoBehaviour
{
    public GameObject[] lista;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < lista.Length; i++)
            {
                if(lista[i]!=null && lista[i].activeSelf)
                    lista[i].SetActive(false);
            }
        }
    }
}
