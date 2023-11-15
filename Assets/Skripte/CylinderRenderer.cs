using System;
using Game.Movement;
using UnityEngine;

public class CylinderRenderer : MonoBehaviour
{
    public SwingingDone swing;

    LineRenderer lineRenderer;
    public bool collidedRuka = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 startPos = lineRenderer.GetPosition(i);
            Vector3 endPos = lineRenderer.GetPosition(i + 1);

            // Perform the raycast
            if (Physics.Raycast(startPos, endPos - startPos, out RaycastHit hitInfo, Vector3.Distance(startPos, endPos)))
            {
                // Check if the raycast hit the desired game object
                if (hitInfo.collider.gameObject.CompareTag("Ruka"))
                {
                    // Destroy the Line Renderer component
                    //Destroy(lineRenderer);
                    swing.StopSwing();
                    // Optionally, destroy the entire game object if needed
                    // Destroy(gameObject);
                    
                    // Break out of the loop to only destroy the line once
                    break;
                }
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ledge"))
    //    {
    //        if (collidedRuka == false)
    //        {
    //            swing.StopSwing();
    //            Destroy(lineRenderer);
    //            //lineRenderer.enabled = false;
    //            collidedRuka= true;
    //        }
    //        
//
    //    }
    //}
}
