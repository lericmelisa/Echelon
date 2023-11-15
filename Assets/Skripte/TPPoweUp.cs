using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Game.Movement;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TPPoweUp : MonoBehaviour
{
    public Throwing tpScript;

    public TimeManipulation tm;

    public SwingingDone swinging;
    public GameObject krug;
    public Material[] materials;

    // Start is called before the first frame update
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
            if (tm.GetComponent<TimeManipulation>().enabled == true)
                tm.GetComponent<TimeManipulation>().enabled = false;
            if (swinging.GetComponent<SwingingDone>().enabled == true)
                swinging.GetComponent<SwingingDone>().enabled = false;
            Renderer krugRenderer = krug.GetComponent<Renderer>();
            krugRenderer.sharedMaterials = materials;
            tpScript.GetComponent<Throwing>().enabled = true;
            Destroy(gameObject);

        }
    }
    
}
