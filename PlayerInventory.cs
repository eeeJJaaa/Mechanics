using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

   
    public RectTransform guiTransform; //tsnasform of the canvas of the UI       
    public Transform playerCamera;

    public Component panel;
        
    public GameObject acidButton;
    public GameObject bombButton;
    public GameObject snakeButton;

    public List<string> playerInventoryList = new List<string>(); //list for picked up items // uses string from Trigger script
    
    GameObject aButton;
    GameObject bButton;
    GameObject sButton;


    float listCount;


    void Start()
    {

        //acidButton.GetComponent<Button>().enabled = false;
        //acidButton.GetComponent<Image>().enabled = false;

        //bombButton.GetComponent<Button>().enabled = false;
        //bombButton.GetComponent<Image>().enabled = false;

        //snakeButton.GetComponent<Button>().enabled = false;
        //snakeButton.GetComponent<Image>().enabled = false;

        panel.gameObject.SetActive(false);

    }


    void Update()
    {
              


        if (playerInventoryList.Count != listCount)
        {
            UpdateInventory();
        }


        if (Input.GetButton("Fire2"))  //GetButtonDown only returns on the down, GetButton returns constantly while down
        {
            //Vector3 centre = new Vector3(0, 0, 0);
            //Vector3 left = new Vector3(67, 0, 0);
            //Vector3 right = new Vector3(-72, 0, 0);
            //acidButton.GetComponent<RectTransform>().anchoredPosition = centre;
            //bombButton.GetComponent<RectTransform>().anchoredPosition = left;

            panel.gameObject.SetActive(true); //Activates the player invent panel


            RectTransform transform = guiTransform.GetComponent<RectTransform>(); // places the transform of the gui into "transform"
            transform.LookAt(playerCamera);                                       // tells the gui transform to face the camera
            
        }


        if (Input.GetButtonUp("Fire2"))
        {
            Debug.Log("Closed Player Inventory");

            panel.gameObject.SetActive(false);

        }


    }

    private void UpdateInventory()
    {

        if (playerInventoryList.Count > listCount)
        {

            if (playerInventoryList[playerInventoryList.Count - 1] == "Bomb") //gets the last element of the list
            {
                GameObject bButton = (GameObject)Instantiate(bombButton);
                bButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                bButton.GetComponent<Image>().enabled = true;
            }

            if (playerInventoryList[playerInventoryList.Count - 1] == "Acid")
            {
                GameObject aButton = (GameObject)Instantiate(acidButton);
                aButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                aButton.GetComponent<Image>().enabled = true;
            }

            if (playerInventoryList[playerInventoryList.Count - 1] == "Snake")
            {
                GameObject sButton = (GameObject)Instantiate(snakeButton);
                sButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                sButton.GetComponent<Image>().enabled = true;
            }

        }
        else //if an item has been removed from the list the gui is redrawn
        {
            foreach (string playerInventoryList in playerInventoryList)
            {
                if (playerInventoryList == "Bomb") //gets the last element of the list
                {
                    GameObject bButton = (GameObject)Instantiate(bombButton);
                    bButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                    bButton.GetComponent<Image>().enabled = true;
                }

                if (playerInventoryList == "Acid")
                {
                    GameObject aButton = (GameObject)Instantiate(acidButton);
                    aButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                    aButton.GetComponent<Image>().enabled = true;
                }

                if (playerInventoryList == "Snake")
                {
                    GameObject sButton = (GameObject)Instantiate(snakeButton);
                    sButton.transform.SetParent(panel.transform, false); //MAKE SURE THE PANEL HAS A LAYOUT COMPONENT IN THE INSPECTOR
                    sButton.GetComponent<Image>().enabled = true;
                }
            }
        }



        listCount = playerInventoryList.Count;

    }
}
