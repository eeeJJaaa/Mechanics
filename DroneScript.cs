using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneScript : MonoBehaviour
{


    public GameObject[] dronePoints;        //waypoint array
    public float droneSpeed = 15;           //speed of drone
    GameObject[] closestWaypoint = new GameObject[1];

    GameObject targetWaypoint;
    GameObject nextWaypoint;
    

    void Start()
    {
        dronePoints = GameObject.FindGameObjectsWithTag("DroneWaypoint"); //finds and loads all tagged drone waypoints

        FindClosestWaypoint(); // finds the closest waypoint when drone is spawned


    }


    void Update()
    {
        Move();
                   

    }




    private void Move() 
    {

        if(Vector3.Distance(transform.position, targetWaypoint.transform.position) > 0.1f) // if drone isnt within a range of the target waypoint
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, droneSpeed * Time.deltaTime); // moves drone towards target
        }
        else 
        {
            GetNextWaypoint(); //if the drone gets to the target the next waypoint is called

        }
               


    }

    private void GetNextWaypoint()
    {

        for (int i = 0; i < dronePoints.Length; i++) //cycles through the waypoint array
        {
            if(targetWaypoint.name == dronePoints[i].name) //when the target waypoint is found within the array this loop engages
            {
                               
                if (dronePoints[i] != dronePoints[dronePoints.Length-1]) //if the current waypoint isnt the last waypoint of the array
                {
                    targetWaypoint = dronePoints[i + 1]; //gets the next waypoint in the array
                    return; //ends the loop
                }
                else                                    // if the current waypoint is the last                               
                {
                    targetWaypoint = dronePoints[0];    //makes the target waypoint the first in the array
                    return;
                }
            }
           
        }



    }



    private void FindClosestWaypoint()
    {

        foreach (GameObject dronePoints in dronePoints)
        {

            if (closestWaypoint[0] == null)
            {
                closestWaypoint[0] = dronePoints;
            }
            else if (Vector3.Distance(gameObject.transform.position, dronePoints.transform.position) < Vector3.Distance(gameObject.transform.position, closestWaypoint[0].transform.position))
            {
                closestWaypoint[0] = dronePoints;
            }

        }

        targetWaypoint = closestWaypoint[0];

    }





}
