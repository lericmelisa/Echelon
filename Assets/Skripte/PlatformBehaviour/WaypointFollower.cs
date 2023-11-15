using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{

    [SerializeField] 
    private GameObject[] waypoints;

    
    //cause waypoints are arrray we need to know which waypoint we are currently moving towards,its index
     int currentWaypointIndex = 0;


     //speed of moving object
     [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update()
    {
        
        //calcuates distance from waypoint .1f cause if we put 0 it might happen it dont detect object(waypoint)
        //if we touch it we switch to next waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0;

        } 
        //vector 3 defines calculates new position between two game objects
                 transform.position =
                     Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed*Time.deltaTime);
                 
                 
        
    }
}
