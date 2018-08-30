using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class VRMovement : MonoBehaviour
{

    //For use with the Vive mainly. Probably can adapt it for other uses too though.
    //This script is to keep rotations of the player and camera (head) synced. Also eventually (soon™) auto-rotation functionality will be implemented.

    private GameObject CameraEye;
    private GameObject CameraRig;
    private GameObject Player;

    private Quaternion HMDLocalRotation; //Current position of HMD.
    private Quaternion HMDHomeForward; //This is the location that the HMD should be in when it is facing the monitor or forward.
    private Quaternion HMDLeftTurnLimit;
    private Quaternion HMDRightTurnLimit;

    public SteamVR_Camera steamCam; //Put the Steam Camera in here.


    private Vector3 movement;
    private Rigidbody playerRigidBody;

    public float speed = 2f;
    public float turnSmoothing = 15f;

    float moveHorizontal;
    float moveVertical;


    private Animator myAnimator;


    


    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }




    void Start()
    {
        CameraEye = GameObject.Find("Camera (eye)");
        CameraRig = GameObject.Find("[CameraRig]");
        Player = GameObject.Find("Player");

        myAnimator = GetComponentInChildren<Animator>();

        

    }

    private void Update()
    {

        float verticalVelocity = playerRigidBody.velocity.y; // gets the vertical speed of the player
        
        var horizontalVelocity = Vector3.Dot(playerRigidBody.velocity, transform.forward.normalized); //gets the horizontal speed of the player



        //if (playerRigidBody.velocity.magnitude > 2) //triggers the forceback animation if velocity of character is over 2
        if (horizontalVelocity > 10f || horizontalVelocity < -10f) //triggers the forceback animation if the horizontal speed is over or under the threshold
        {
            myAnimator.SetBool("Force", true);
            myAnimator.speed = 10; //increases the speed of the animator so that the forceback looks instant
        }
        else
        {
            myAnimator.SetBool("Force", false);
            myAnimator.speed = 1;
        }

        if (verticalVelocity < -2f)
        {
            myAnimator.SetBool("Falling", true);
            Debug.Log("FALLING");
           
        }
        else
        {
            myAnimator.SetBool("Falling", false);
        }
    }



    void FixedUpdate()
    {

        if (Input.GetAxis("HorizontalJ") != 0f || (Input.GetAxis("VerticalJ") != 0))
        {
            Move();
        }
        else
        {
            myAnimator.SetInteger("AnimPar", 0);
        }
        
 

    }


    private void Move()
    {

        Vector3 right = Vector3.Cross(Vector3.up, CameraEye.transform.forward);
        Vector3 forward = Vector3.Cross(Vector3.up, right);
        Vector3 movement = Vector3.zero;

        movement += right * (Input.GetAxis("HorizontalJ") * speed * Time.deltaTime);
        movement += forward * (Input.GetAxis("VerticalJ") * speed * Time.deltaTime);
        //movement.y = playerRigidBody.velocity.y;

        //float h = Input.GetAxisRaw("HorizontalJ");  //Have switched the gamepad axis to HorizontalJ and VerticalJ and removed them from the standard horizontal/vertical so that only the Dpad
        //float v = Input.GetAxisRaw("VerticalJ");

        


        playerRigidBody.MovePosition(transform.position + movement);

        //playerRigidBody.AddForce(movement, ForceMode.Impulse);   // THIS IS THE CORRECT WAY TO MOVE A RIGIDBODY

        //playerRigidBody.velocity = (movement * speed); // This screws up gravity by overwriting the Y value so need to use the line below
        //playerRigidBody.velocity = new Vector3 (Time.deltaTime * movement.x * speed, playerRigidBody.velocity.y, Time.deltaTime * movement.z * speed);  //speed has to be up really high

        myAnimator.SetInteger("AnimPar", 1); //sets the "AnimPar" integer to 1 in the animator. Have to have "exit times" etc unchecked in the animator transition inspector


        


        if (movement.x != 0)
        {
            Rotating();
        }


    }



    void Rotating()
    {
        Vector3 right = Vector3.Cross(Vector3.up, CameraEye.transform.forward);
        Vector3 forward = Vector3.Cross(Vector3.up, right);
        
        Vector3 targetDirection = new Vector3();

        targetDirection += right * (Input.GetAxis("HorizontalJ"));
        targetDirection += forward * (Input.GetAxis("VerticalJ"));
                
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);
        
        GetComponent<Rigidbody>().MoveRotation(newRotation);



   
    }


    
}