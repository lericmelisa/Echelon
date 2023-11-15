using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject spawnPoint;
    private Transform pozicija;

    // Start is called before the first frame update
    void Start()
    {
        pozicija = GetComponent<Transform>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
        if (pozicija.position.y< 340f || Input.GetKeyDown(KeyCode.R))
        {
            pozicija.position = spawnPoint.transform.position + new Vector3(0, 3, 0);

        }
      
    }
}