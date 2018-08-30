using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneScript : MonoBehaviour
{


    public GameObject[] dronePoints;        //waypoint array
    public float droneSpeed = 15;           //speed of drone
    GameObject closestWaypoint;

    GameObject targetWaypoint;
    GameObject nextWaypoint;


    public GameObject[] players;         // arracy for all the players



    float smoothing = 15f;


    float shootTimer = 0;


    GameObject targetPlayer; 
    GameObject projectile;






    void Start()
    {
        dronePoints = GameObject.FindGameObjectsWithTag("DroneWaypoint"); //finds and loads all tagged drone waypoints

        players = GameObject.FindGameObjectsWithTag("Player"); //finds and loads all tagged players

        FindClosestWaypoint(); // finds the closest waypoint when drone is spawned

        projectile = Resources.Load<GameObject>("Projectile"); // loads in the projectile prefab from the resources folder
         

    }


    void Update()
    {
        Move();
        FindTarget();
   
        if ( targetPlayer != null && shootTimer < Time.time) // if there is a target and the timer is ok then.....
        {

            FireAtEnemy();
            shootTimer = Time.time + 0.2f; // resets the timer?? the float is the delay between shots
            

        }



        
    }


    private void Move() 
    {

        if(Vector3.Distance(transform.position, targetWaypoint.transform.position) > 0.1f) // if drone isnt within a range of the target waypoint
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, droneSpeed * Time.deltaTime); // moves drone towards target

            Vector3 direction = transform.root.position - transform.position;                                      //
            direction.y = 0;                                                                                       // rotates the drone to face the centre of the building
            var rotation = Quaternion.LookRotation(direction);                                                     //
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothing);       //

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

        foreach (GameObject dronePoints in dronePoints)  //cycles through the waypoints in the array
        {

            if (closestWaypoint == null) //if the closestWaypoint  variable is empty the load in the first waypoint
            {
                closestWaypoint = dronePoints;
            }
            else if (Vector3.Distance(gameObject.transform.position, dronePoints.transform.position) < Vector3.Distance(gameObject.transform.position, closestWaypoint.transform.position)) //compares distances
            {
                closestWaypoint = dronePoints; //if the waypoint is closer than the previous saved waypoint then the new one is loaded in
            }

        }

        targetWaypoint = closestWaypoint; // loads it into targetWaypoint

    }


    private void FindTarget()
    {

        foreach (GameObject players in players) //cycles through all the players in the array
        {
            if (Vector3.Distance(transform.position, players.transform.position) < 20f) // if the player is closer than the float then it becomes the targetPlayer
            {
                targetPlayer = players;
                               

            }
            else // if no player is in range then the target is null
            {
                targetPlayer = null;
            }


        }
        
    }

    private void FireAtEnemy() //instantiate the projectile
    {
              
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.LookRotation(targetPlayer.transform.position - transform.position), transform.root) as GameObject;
              

    }




}
