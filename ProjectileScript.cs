using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{


    GameObject explosionEffect; 

    void Start()
    {

        explosionEffect = Resources.Load<GameObject>("SmallExplosionEffect"); // loads in the explosion from the RESOURCES folder
    }

    
    void Update()
    {
        transform.position += transform.forward * 10 * Time.deltaTime; //moves the projectile
    }


    private void OnCollisionEnter(Collision collision) // see quill18 collision video
    {


        //Instantiate(explosionEffect, transform.position, transform.rotation);
        ////gameObject.GetComponent<Rigidbody>().AddExplosionForce(13f, transform.position, 20f, .0f, ForceMode.Impulse); // not working yet
        //Destroy(gameObject);



    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag != "Drone" && other.gameObject.tag != "BreakableGlass" )
        {
            Debug.Log(other);

            Instantiate(explosionEffect, transform.position, transform.rotation);
            //gameObject.GetComponent<Rigidbody>().AddExplosionForce(13f, transform.position, 20f, .0f, ForceMode.Impulse); // not working yet
            Destroy(gameObject);
        }

    }





}
