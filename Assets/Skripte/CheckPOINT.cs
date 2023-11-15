using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPOINT : MonoBehaviour
{
    // Start is called before the first frame update
    private Reset respawn;

    private void Awake()
    {

        respawn = GameObject.FindGameObjectWithTag("Player").GetComponent<Reset>();
    }

    void Start()
    {


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            respawn.spawnPoint = this.gameObject;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
