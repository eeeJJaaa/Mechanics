using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemDatabase : MonoBehaviour
{

    //public List<string> Items = new List<string>(); //list
    //public string[] items = { "Bomb", "Snakes", "TripWire"}; //can also be declared this way
    //public GameObject[] gameObjects = new GameObject[5];
    //public GameObject[] gameObjectsArray;



    public string[] items = new string[3]; //array of items that are distributed at the start of the game
    public List<GameObject> gameObjectsList = new List<GameObject>();



    private string myItem;

    GameObject myObject;
    Trigger myObjectScript; //loads the script "Trigger" into myObjectScript


    
    private void Start()
    {
        items[0] = "Bomb";
        items[1] = "Snake";
        items[2] = "Acid";
        


        //gameObjectsArray = GameObject.FindGameObjectsWithTag("Desk"); //finds all objects tagged "desk" and fills the array       
        //gameObjects[0] = GameObject.FindGameObjectWithTag("Desk");
        //gameObjects[1] = GameObject.FindGameObjectWithTag("Desk");



        TagList();
        AllocateItems();
    }

    private void TagList()
    {
        foreach (GameObject taggedObject in GameObject.FindGameObjectsWithTag("Desk"))
        {

            gameObjectsList.Add(taggedObject); //adds each tagged GameObject found to list "gameObjects"
        }
    }

    private void AllocateItems()
    {

        foreach(string items in items) //for each of the items in the array it does this...
        {

            //myObject = gameObjectsArray[UnityEngine.Random.Range(0, gameObjectsArray.Length)]; //gets random gameobject from the array

            myObject = gameObjectsList[UnityEngine.Random.Range(0, gameObjectsList.Count)]; //gets random gameobject from list
            myObjectScript = myObject.GetComponent<Trigger>(); // script attached to gameobject and containing the variable you want to change
            myObjectScript.itemSlot = items; // places the string from "items" array into the "slot" of the GameObject
            gameObjectsList.Remove(myObject); //removes the GameObject from list to avoid duplicates
            
        }

        
    }
    
}