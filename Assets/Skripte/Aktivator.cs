using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aktivator : MonoBehaviour
{
    public GameObject unutrasnja;
    public WaypointFollower wpf;
    public bool aktiviran;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            wpf.enabled = true;
            unutrasnja.SetActive(true);
            aktiviran = true;
        }
    }
}
