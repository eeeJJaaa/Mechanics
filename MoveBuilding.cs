using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBuilding : MonoBehaviour
{


    //bool tilted = false;
    //bool right = false;    // snapping left and right variables
    //bool left = false;

    





    void Start()
    {
        
    }


    void Update()
    {

        float twist = Input.GetAxis("RightStick");  //if the rightstick is continuosly outputting in one direction lower the senistivity(10) and dead field(0.1) in the inputmanager.
                                                    //right stick is 3rd and 4th axis

        transform.Rotate(0f, twist, 0f); //rotates building

        
        transform.Translate(0f, Input.GetAxis("RightStickUp"), 0f); // lifts or lowers building


 




        #region Snapping
        //////////////////////////////// snapping left and right /////////////////////////////////////////////
        //if ((Input.GetAxis("RightStick") == 1) && (tilted == false))
        //{
        //    Transform buildingTransform = transform;
        //    buildingTransform.Rotate(0f, 90f, 0f);
        //    left = true;
        //    tilted = true;

        //}

        //if ((Input.GetAxis("RightStick") == -1) && (tilted == false))
        //{
        //    Transform buildingTransform = transform;
        //    buildingTransform.Rotate(0f, -90f, 0f);
        //    right = true;
        //    tilted = true;

        //}

        //if ((Input.GetAxis("RightStick") == 0) && (tilted == true))
        //{
        //    tilted = false;

        //    if (right == true)
        //    {
        //        Transform buildingTransform = transform;
        //        buildingTransform.Rotate(0f, 90f, 0f);
        //        right = false;
        //    }
        //    else
        //    {
        //        Transform buildingTransform = transform;
        //        buildingTransform.Rotate(0f, -90f, 0f);
        //        left = false;
        //    }
        //}
    }   /////////////////////////////////////////////////////////////////////////////////////////////////////////
    #endregion



}
