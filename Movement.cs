using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //Vector3 velocity;
    //public float movespeed = 2f;


    bool jump = false;


    public float speed = 2f;
    //public float runSpeed = 5f;
    public float turnSmoothing = 15f;

    public Camera mainCam;

    private Vector3 movement;
    private Rigidbody playerRigidBody;


    
    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    
    void Start()
    {



    }


    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }


    void FixedUpdate()
    {

        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        //velocity = new Vector3(horizontal, 0f, vertical);
        //transform.Translate(velocity * movespeed * Time.deltaTime);


        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");

        Move(lh, lv);


        if (jump == true)
        {
            Debug.Log("jump");

           
            Vector3 jumpposition = new Vector3(transform.position.x, 50f, transform.position.z);


            //Vector3 movementup = new Vector3(horizontal, 40f, vertical);
            //transform.Translate(movementup * Time.deltaTime * movespeed); /// dont do this, you are using a rigidbody whoch should use the physics engine to move
            //transform.position = Vector3.Lerp(transform.position, air, 0.02f);

            transform.position = Vector3.MoveTowards(transform.position, jumpposition, 0.85f);
            jump = false;

            


        }



    }

    void Move(float lh, float lv)
    {
        movement = Camera.main.transform.TransformDirection(movement);
        movement.Set(lh, 0f, lv); //this must go after the camera transform call so that the y value is reset to 0...otherwise the player climbs into air

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    movement = movement.normalized * runSpeed * Time.deltaTime;
        //}
        //else
        //{
            movement = movement.normalized * speed * Time.deltaTime;
        //}

        playerRigidBody.MovePosition(transform.position + movement);


        if (lh != 0f || lv != 0f)
        {
            Rotating(lh, lv);
        }
    }


    void Rotating(float lh, float lv)
    {
        Vector3 targetDirection = new Vector3(lh, 0f, lv);

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }




}
