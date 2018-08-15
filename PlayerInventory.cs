using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

   
    public RectTransform guiTransform; //tsnasform of the canvas of the UI       
    public Transform playerCamera;

    public Component panel;
        
    public GameObject acidButton;
    public GameObject bombButton;
    public GameObject snakeButton;
       
    GameObject aButton;
    GameObject bButton;
    GameObject sButton;

    Button firstButton;
    Button secondButton;
    Button thirdButton;



        
    float listCount; //counter for playerInventoryList so can tell when the list changes

    bool inventOpen; //bool for if the invent is open
    
    public List<string> playerInventoryList = new List<string>(); //list for picked up items // uses string from Trigger script
    List<Button> playerCarryList = new List<Button>(); //list for the buttons in the players UI above there head


    PlayerInteract playerInteractScript;


    string trapType;





    
    void Start()
    {

        //acidButton.GetComponent<Button>().enabled = false;
        //acidButton.GetComponent<Image>().enabled = false;

        //bombButton.GetComponent<Button>().enabled = false;
        //bombButton.GetComponent<Image>().enabled = false;

        //snakeButton.GetComponent<Button>().enabled = false;
        //snakeButton.GetComponent<Image>().enabled = false;

        panel.gameObject.SetActive(false);
        inventOpen = false;

    }


    void Update()
    {
        
        if (playerInventoryList.Count != listCount)
        {
            UpdateInventory();
        }


        if (Input.GetButton("Fire2"))  //GetButtonDown only returns on the down, GetButton returns constantly while down
        {

            
            if (inventOpen == false)
            {
                
                panel.gameObject.SetActive(true); //Activates the player invent panel

                if (firstButton != null)
                {
                    firstButton.GetComponent<Selectable>().Select(); // selects the button
                }

            }

            RectTransform transform = guiTransform.GetComponent<RectTransform>(); // places the transform of the gui into "transform"
            transform.LookAt(playerCamera);                                       // tells the gui transform to face the camera

            inventOpen = true;

        }


        if (Input.GetButtonUp("Fire2"))
        {
            Debug.Log("Closed Player Inventory");


            panel.gameObject.SetActive(false);

            inventOpen = false;

        }


    }

    private void ButtonClicked(int whichButton) //needs an argument of type int which it then loads into "whichButton"
    {

        trapType = playerInventoryList[whichButton];
        
        playerInteractScript = gameObject.GetComponentInParent<PlayerInteract>(); // gets the script PlayerInteract which is in the Parent of the PlayerInventory PREFAB?OBJECT 

        playerInteractScript.SendMessage("ButtonClicked", trapType); //runs the ButtonClicked method in the script "PlayerInteract" and sends the string "trapType"

    }

    #region Update Inventory and GUI

    private void UpdateInventory()
    {

        if (playerInventoryList.Count < 4)
        {
                        
            if (playerInventoryList.Count > listCount)
            {

                if (playerInventoryList[playerInventoryList.Count - 1] == "Bomb") //gets the last element of the list
                {
                    GameObject bButton = (GameObject)Instantiate(bombButton);
                    bButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                    bButton.GetComponent<Image>().enabled = true;
                    bButton.GetComponent<Button>().enabled = true;
                    playerCarryList.Add(bButton.GetComponent<Button>());
                }

                if (playerInventoryList[playerInventoryList.Count - 1] == "Acid")
                {
                    GameObject aButton = (GameObject)Instantiate(acidButton);
                    aButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                    aButton.GetComponent<Image>().enabled = true;
                    aButton.GetComponent<Button>().enabled = true;
                    playerCarryList.Add(aButton.GetComponent<Button>());
                }

                if (playerInventoryList[playerInventoryList.Count - 1] == "Snake")
                {
                    GameObject sButton = (GameObject)Instantiate(snakeButton);
                    sButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                    sButton.GetComponent<Image>().enabled = true;
                    sButton.GetComponent<Button>().enabled = true;
                    playerCarryList.Add(sButton.GetComponent<Button>());
                }

            }
            else //if an item has been removed from the list the gui is redrawn
            {

                playerCarryList.Clear(); //clears the player carrylist

                foreach (Transform child in panel.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (string playerInventoryList in playerInventoryList)
                {
                    if (playerInventoryList == "Bomb") //gets the last element of the list
                    {
                        GameObject bButton = (GameObject)Instantiate(bombButton);
                        bButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                        bButton.GetComponent<Image>().enabled = true;
                        bButton.GetComponent<Button>().enabled = true;
                        playerCarryList.Add(bButton.GetComponent<Button>());
                    }

                    if (playerInventoryList == "Acid")
                    {
                        GameObject aButton = (GameObject)Instantiate(acidButton);
                        aButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                        aButton.GetComponent<Image>().enabled = true;
                        aButton.GetComponent<Button>().enabled = true;
                        playerCarryList.Add(aButton.GetComponent<Button>());
                    }

                    if (playerInventoryList == "Snake")
                    {
                        GameObject sButton = (GameObject)Instantiate(snakeButton);
                        sButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                        sButton.GetComponent<Image>().enabled = true;
                        sButton.GetComponent<Button>().enabled = true;
                        playerCarryList.Add(sButton.GetComponent<Button>());
                    }
                }
            }
        }
        else
        {
            Debug.Log("INVENTORY FULL");
        }

        if (playerCarryList.Count == 1)
        {
            firstButton = playerCarryList[0]; //gets the first button in the list

            firstButton.onClick.RemoveAllListeners(); //Removes any existing listeners. Wont throw an error if no listeners exist yet
            firstButton.onClick.AddListener(delegate { ButtonClicked(0); }); //if button is clicked calls method "ButtonClicked" and sends int "1"
    
        }



        if (playerCarryList.Count == 2)
        {
            firstButton = playerCarryList[0]; //gets the first button in the list
            firstButton.onClick.RemoveAllListeners(); //Removes any existing listeners. Wont throw an error if no listeners exist yet
            firstButton.onClick.AddListener(delegate { ButtonClicked(0); }); //if button is clicked calls method "ButtonClicked" and sends int "1"

            secondButton = playerCarryList[1]; //gets the second button in the list
            secondButton.onClick.RemoveAllListeners(); //Removes any existing listeners. Wont throw an error if no listeners exist yet
            secondButton.onClick.AddListener(delegate { ButtonClicked(1); }); //if button is clicked calls method "ButtonClicked" and sends int "2"
        }



        if (playerCarryList.Count == 3)
        {
            firstButton = playerCarryList[0]; //gets the first button in the list
            //firstButton.GetComponent<Selectable>().Select(); // selects the button
            firstButton.onClick.RemoveAllListeners(); //Removes any existing listeners. Wont throw an error if no listeners exist yet
            firstButton.onClick.AddListener(delegate { ButtonClicked(0); }); //if button is clicked calls method "ButtonClicked" and sends int "1"

            secondButton = playerCarryList[1]; //gets the second button in the list
            secondButton.onClick.RemoveAllListeners(); //Removes any existing listeners. Wont throw an error if no listeners exist yet
            secondButton.onClick.AddListener(delegate { ButtonClicked(1); }); //if button is clicked calls method "ButtonClicked" and sends int "2"


            thirdButton = playerCarryList[2]; //gets the third button in the list
            thirdButton.onClick.RemoveAllListeners(); //Removes any existing listeners. Wont throw an error if no listeners exist yet
            thirdButton.onClick.AddListener(delegate { ButtonClicked(2); }); //if button is clicked calls method "ButtonClicked" and sends int "1"
        }

        
        listCount = playerInventoryList.Count;






    }

    #endregion


















































}
