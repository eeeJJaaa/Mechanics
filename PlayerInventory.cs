using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private Button itemButton;

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private RectTransform guiTransform;

    [SerializeField]
    private Transform playerCamera;

    public string itemPickup; //used to change button image from Trigger script



    void Start()
    {
        itemButton.enabled = false;
        itemImage.enabled = false;
     
    }


    void Update()
    {
        if (Input.GetButton("Fire2"))//GetButtonDown only returns on the down, GetButton returns constantly while down
        {
            Debug.Log("Open Player Inventory");


            itemButton.enabled = true;
            itemImage.enabled = true;
            

            RectTransform transform = guiTransform.GetComponent<RectTransform>(); // places the transform of the gui into "transform"
            transform.LookAt(playerCamera);                                       // tells the gui transform to face the camera
            
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            Debug.Log("Closed Player Inventory");


            itemButton.enabled = false;
            itemImage.enabled = false;
            
        }


    }


    private void FixedUpdate()
    {
        






    }
}
