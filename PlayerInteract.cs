using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerInteract : MonoBehaviour
{

    public GameObject currentInteractable = null;

    Trigger triggerTrapSlot;
    PlayerInventory playerInventory;

    //string trapSlot;



    private void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)  // Only one of the two objects needs to have a trigger but the other needs at least a collider
    {
        if (other.CompareTag("Interactable"))  //checks to see if the object the player has triggered with is an "Interactable"
        {
            Debug.Log(other.name);

            currentInteractable = other.gameObject; // loads the current interactable object into currentInteractable so it can be accessed
        }

                   
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
    }

    public void ButtonClicked(string trapType) //called from the SENDMESSAGE in "PlayerInventory" script and a string is sent and stored in "trapType"
    {
        Debug.Log("Button was CLICKED");
        Debug.Log(trapType);

        if (currentInteractable != null)
        {
            if (currentInteractable.CompareTag("Interactable"))
            {
                triggerTrapSlot = currentInteractable.GetComponent<Trigger>(); // gets the script Trigger from the interactable 


                if (triggerTrapSlot.trapSlot == "")
                {
                    Debug.Log("trap " + trapType + " has been planeted");
                    triggerTrapSlot.trapSlot = trapType;

                    playerInventory = gameObject.GetComponentInChildren<PlayerInventory>();  //
                    playerInventory.playerInventoryList.Remove(trapType); //removes the matching string of trapty[e from playerInventoryList in the PlayerInventory script // wont work if multiple traps of the
                                                                          // same type are in the list?? or maybe it doesnt matter?
                  
                }
                else
                {
                    Debug.Log("YOU SHOULD HAVE TRIGGERED A TRAP?????");
                }



            }

        }



    }

   
}
