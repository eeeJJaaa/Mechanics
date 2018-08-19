using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassKinematics : MonoBehaviour   //// THIS SCRIPT IS PLACED ON THE BROKEN SHARDS PREFABS
{

    public float delay = 0.09f; //good timing for an explosive force of 20 on bombs

    IEnumerator Start()
    {

        Rigidbody thisRigidbody = GetComponent<Rigidbody>();
        Collider thisCollider = GetComponent<Collider>();


        yield return new WaitForSeconds(delay);

        thisRigidbody.isKinematic = false;
        thisCollider.enabled = true;
        


    }

   
    void Update()
    {

    }
}



// Should try and set it up so that if the impact is below a certain force the glass only cracks
// at the moment the glass breaks on any trigger because i replaced the on collision method with ontrigger method of the main panel
// and set a delay of when the shards of glass become rigidbodies and when the collider is enabled


    // ended up placing another collider on the window, slightly bigger than the original, and making it a trigger....so just walking up to it hits the original collider and the  
    // window doesnt break, but if you are going over a certain speed the trigger will break the window and delay the rigidbodies etc