﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{

   


    bool triggered;

    #region BomB Variables
    public float radius = 4F;  //these values seem to work ok for ForceMode.Impulse
    public float power = 13F; //these values seem to work ok
    public float upforce = .7f; //these values seem to work ok
    bool explosion = false;
    public GameObject explosionEffect; //Explosion particle effect
    #endregion




    void Start()
    {
        
    }


    void Update()
    {

    }

    public void Bomb()
    {
        explosion = true;
       
    }

    private void FixedUpdate()
    {


        #region Bomb
        if (explosion == true)
        {
            Vector3 explosionPos = transform.position; //defines position of explosion


            Instantiate(explosionEffect, transform.position, transform.rotation); //spawns eplosion // make sure "destroy" on stop is selected in Inspector to prevent looping


            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, upforce, ForceMode.Impulse);

                explosion = false;

            }

        }

        #endregion

        

    }

    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
 
        
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;


    }

    //////////////////// another way to apply an explosive force
    //if(explosionForce == true)
    //{
    //    rb.AddForce(rb.transform.forward * thrust); // adds force to rigidbody mimic explosion force
    //    explosionForce = false;
    //}





}