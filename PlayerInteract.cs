using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerInteract : MonoBehaviour
{

    public GameObject[] tripWireArray = new GameObject[2]; //array for the origin and then the destination of the tripwire
    public bool tripWirePossible = false;
    public bool tripWirePlanted = false;
    public GameObject tripWire;
    public GameObject tripOrigin;


    public GameObject drone; //load in the drone prefab



    public GameObject currentInteractable = null;
    public GameObject currentObject = null;

       

    Trigger triggerTrapSlot;
    PlayerInventory playerInventory;

    

    private void Update()
    {
        
        if (Input.GetButtonDown("Submit") && currentObject != tripWireArray[0] && currentObject != null && tripWirePlanted == true ) //somehow use a WHILE loop for while the player is planting a tripwire?
        {
            Vector3 tripDestinationPos = currentObject.transform.position;   // gets the vector3 of the destination  object

            if (Vector3.Distance(tripWireArray[0].transform.position, tripDestinationPos) < 7f)
            {
                
                tripDestinationPos.y = tripDestinationPos.y + 1f;                                                  // lifts the pink ball above the current object
                Instantiate(tripOrigin, tripDestinationPos, tripWire.transform.rotation, currentObject.transform); //instantiates the pink ball above the current object
                Vector3 middleWire = ((currentObject.transform.position + tripWireArray[0].transform.position)/2); // gets the middle position between the first and second object
                


                Vector3 lookPos = (currentObject.transform.position - tripWireArray[0].transform.position);     //
                lookPos.y = 89; //no idea why this is needed but it is                                          //   NO IDEA WHATS GOING ON HERE...but it kinda rotates the wire inbetween the objects
                Quaternion wireRotation = Quaternion.LookRotation(lookPos);                                     //
         




                Instantiate(tripWire, middleWire, wireRotation, currentObject.transform); //instantiates the trip wire

                

                tripWirePlanted = false;
                

            }
            else
            {
                Debug.Log("DESTINATION TOO FAR AWAY");

            }
        }

        
    }



    private void OnTriggerEnter(Collider other)  // Only one of the two objects needs to have a trigger but the other needs at least a collider
    {
        if (other.CompareTag("Interactable"))  //checks to see if the object the player has triggered with is an "Interactable"
        {
            Debug.Log(other.name);

            currentInteractable = other.gameObject; // loads the current interactable object into currentInteractable so it can be accessed
        }


        currentObject = other.gameObject;
  
                           
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))  //checks to see if the object the player has triggered with is an "Interactable"
        {
            if (other.gameObject == currentInteractable) //checks to see if the trigger weve just left is of the current object we are interacting with
            {
                currentInteractable = null; //if it was the current object then it is removed
            }

           
        }

        currentObject = null;
        

    }
    
    private void OnCollisionEnter(Collision other)  // to stop the floow being detected maybe tag the entire level as "Floor" and have the method ignore floors "!= "floor""
    {
        currentObject = other.gameObject;

        if (currentObject.GetComponent<Collider>() != null)
        {
            tripWirePossible = true;
        }



    }

    private void OnCollisionExit(Collision other)
    {
        currentObject = null;


        tripWirePossible = false;
    }
    
    public void ButtonClicked(string trapType) //called from the SENDMESSAGE in "PlayerInventory" script and a string is sent and stored in "trapType"
    {
        
        Debug.Log(trapType);

        if (trapType == "Drone")
        {
            Instantiate(drone, transform.position, drone.transform.rotation, transform.root); //instantiates the drone and places it in the root parent...the building
            playerInventory = gameObject.GetComponentInChildren<PlayerInventory>();  //
            playerInventory.playerInventoryList.Remove(trapType); //removes the matching string of traptype from playerInventoryList in the PlayerInventory script // wont work if multiple traps of the
                                                                  // same type are in the list?? or maybe it doesnt matter?

        }




        if (currentInteractable != null)
        {
            if (currentInteractable.CompareTag("Interactable"))
            {
                triggerTrapSlot = currentInteractable.GetComponent<Trigger>(); // gets the script Trigger from the interactable 


                if (triggerTrapSlot.trapSlot == "" && trapType != "TripWire") //checks to see if trapslot is empty or if the player is trying to plant a tripwire
                {
                    Debug.Log("trap " + trapType + " has been planeted");
                    triggerTrapSlot.trapSlot = trapType;

                    playerInventory = gameObject.GetComponentInChildren<PlayerInventory>();  //
                    playerInventory.playerInventoryList.Remove(trapType); //removes the matching string of traptype from playerInventoryList in the PlayerInventory script // wont work if multiple traps of the
                                                                          // same type are in the list?? or maybe it doesnt matter?
                  
                }
                else
                {
                    Debug.Log("Was a TRIPWIRE");
                }



            }
            
        }
        
            
        if (tripWirePossible == true && trapType == "TripWire")
        {

            Vector3 tripOriginPos = currentObject.transform.position;   // gets the vector3 of the current object
            tripOriginPos.y = tripOriginPos.y + 1f;                     // lifts the pink ball above the current object
            Instantiate(tripOrigin, tripOriginPos, tripWire.transform.rotation, currentObject.transform); //instantiates the pink ball above the current object
            tripWireArray[0] = currentObject;                           //loads the current object into the array
            tripWirePlanted = true;

                

            playerInventory = gameObject.GetComponentInChildren<PlayerInventory>();
            playerInventory.playerInventoryList.Remove(trapType); //removes the matching string of trapty[e from playerInventoryList in the PlayerInventory script // wont work if multiple traps of the
                                                                    // same type are in the list?? or maybe it doesnt matter?
            

        }
    



    }

   
}
