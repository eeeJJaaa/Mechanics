using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Trigger : MonoBehaviour
{

    bool triggered;

    public Button itemButton;
    public Image buttonImage;
    public Component buttonComponent;

    public Sprite bombSprite;
    public Sprite snakeSprite;
    public Sprite acidSprite;
    public Sprite tripwireSprite;
    public Sprite droneSprite;

    Traps trapCall;

    public string itemSlot; //used for storing items randomly allocated at start of game
    public string trapSlot = ""; //used for placing traps on item

    PlayerInventory playerInventory; //loads access to PlayerInventory script into playerinventory
    GameObject playerUnit;


    void Start()
    {
        itemButton.enabled = false;
        buttonImage.enabled = false;
             
    }

    
    void Update()
    {


        if (Input.GetButtonDown("Fire1") && (triggered == true) && (trapSlot != ""))
        {

            if (trapSlot == "Bomb")
            {
                
                trapCall = gameObject.GetComponent<Traps>();
                trapCall.SendMessage("Bomb");
                trapSlot = "";
            }

            Debug.Log("THERE IS A TRAP IN HERE");


        }
        else if (Input.GetButtonDown("Fire1") && (triggered == true) && (itemSlot != ""))
        {
            ItemSpriteSet();

            Button btn1 = buttonComponent.GetComponent<Button>(); //sets btn1 to Button Component defined in the inspector

            itemButton.enabled = true;
            buttonImage.enabled = true;

            btn1.onClick.RemoveAllListeners(); //removes any listeners that may have existed due to buggy players interactions with desk and gui
            btn1.Select(); //focus button
            btn1.onClick.AddListener(ButtonClicked); //if button is clicked calls method "ButtonClicked"

        }
           
    }

    private void ItemSpriteSet()
    {
        Button btn1 = buttonComponent.GetComponent<Button>(); //sets btn1 to Button Component defined in the inspector
        

        if (itemSlot == "Bomb")
        {
            btn1.image.sprite = bombSprite;
            Debug.Log("Button = " + btn1.image.sprite);
        }

        if (itemSlot == "Snake")
        {
            btn1.image.sprite = snakeSprite;
            Debug.Log("Button = " + btn1.image.sprite);
        }

        if (itemSlot == "Acid")
        {
            btn1.image.sprite = acidSprite;
            Debug.Log("Button = " + btn1.image.sprite);
        }

        if (itemSlot == "TripWire")
        {
            btn1.image.sprite = tripwireSprite;
            Debug.Log("Button = " + btn1.image.sprite);
        }

        if (itemSlot == "Drone")
        {
            btn1.image.sprite = droneSprite;
            Debug.Log("Button = " + btn1.image.sprite);
        }

    }

    void ButtonClicked()
    {
        Button btn1 = buttonComponent.GetComponent<Button>(); //sets btn1 to Button Component defined in the inspector

        EventSystem.current.SetSelectedGameObject(null);


        playerUnit = GameObject.FindGameObjectWithTag("Player");        
        playerInventory = playerUnit.GetComponentInChildren<PlayerInventory>(); // script attached to a CHILD of the main gameobject and containing the variable you want to change        
        
        if (playerInventory.playerInventoryList.Count >= 3)
        {
            Debug.Log("CANT PICK UP, INVENTORY FULL");
        }
        else
        {
            playerInventory.playerInventoryList.Add(itemSlot); //adds the string from itemslot in the playerInventory list
        
            btn1.onClick.RemoveListener(ButtonClicked); //removes listener to prevent mulitple clicks and bugs??

            itemButton.enabled = false;
            buttonImage.enabled = false;
            itemSlot = "";

        }

                
    }
    


    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        Debug.Log(triggered);
      
        
    }

    private void OnTriggerExit(Collider other)
    {
        Button btn1 = buttonComponent.GetComponent<Button>(); //sets btn1 to Button Component defined in the inspector

        triggered = false;

        itemButton.enabled = false;
        buttonImage.enabled = false;

        btn1.onClick.RemoveListener(ButtonClicked); //removes listener to prevent mulitple clicks and bugs??
        EventSystem.current.SetSelectedGameObject(null); //deselects the button when the player leaves the desk area

    }



}
