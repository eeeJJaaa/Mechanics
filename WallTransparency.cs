using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{

    GameObject g;

    public List<Renderer> windows = new List<Renderer>(); //creates a list for all the windows on a floor


    
    Renderer vis;

    float visR;
    float visG;
    float visB;

    float windowsR;
    float windowsG;
    float windowsB;


    


    void Start()
    {
        vis = GetComponent<Renderer>();

        windows.AddRange(GetComponentsInChildren<Renderer>()); //gets the renderers for all the windows on the floor
        

        visR = vis.material.color.r;    //GETS THE COLOR OF THE OBJECT
        visG = vis.material.color.g;    //GETS THE COLOR OF THE OBJECT 
        visB = vis.material.color.b;    //GETS THE COLOR OF THE OBJECT
        


    }


    void Update()
    {

    }

    

    private void OnTriggerEnter(Collider other)
    {
        //vis.enabled = false;
        //vis.materials.color.a = Color.clear;

        if (other.CompareTag("Player")) // checks to see if the collision was by the player
        {

            vis.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);                 //////changes render mode to transparent
            vis.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            vis.material.SetInt("_ZWrite", 0);
            vis.material.DisableKeyword("_ALPHATEST_ON");
            vis.material.DisableKeyword("_ALPHABLEND_ON");
            vis.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            vis.material.renderQueue = 3000;

            vis.material.renderQueue = 3000;

            vis.material.color = new Color(visR, visG, visB, 0.5f); // MAKES THE MATERIAL TRANSPARENT AND USES THE ORIGINAL COLOR


            foreach (Renderer windows in windows) //for each window found, alter its transparency
            {

                windows.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);                 //////changes render mode to transparent
                windows.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                windows.material.SetInt("_ZWrite", 0);
                windows.material.DisableKeyword("_ALPHATEST_ON");
                windows.material.DisableKeyword("_ALPHABLEND_ON");
                windows.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                windows.material.renderQueue = 3000;

                windows.material.renderQueue = 3000;

                windowsR = windows.material.color.r;    //GETS THE COLOR OF THE window
                windowsG = windows.material.color.g;    //GETS THE COLOR OF THE window 
                windowsB = windows.material.color.b;    //GETS THE COLOR OF THE window

                windows.material.color = new Color(windowsR, windowsG, windowsB, 0.5f); // MAKES THE MATERIAL TRANSPARENT AND USES THE ORIGINAL COLOR


            }

        }

    }   
    



    private void OnTriggerExit(Collider other)
    {
        //vis.enabled = true;

        if (other.CompareTag("Player")) // checks to see if the collision was by the player
        {
            vis.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);             ////////// CHANGES RENDER MORE TO OPAQUE
            vis.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            vis.material.SetInt("_ZWrite", 1);
            vis.material.DisableKeyword("_ALPHATEST_ON");
            vis.material.DisableKeyword("_ALPHABLEND_ON");
            vis.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            vis.material.renderQueue = -1;


            foreach (Renderer windows in windows)
            {
                windows.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);             ////////// CHANGES RENDER MORE TO OPAQUE
                windows.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                windows.material.SetInt("_ZWrite", 1);
                windows.material.DisableKeyword("_ALPHATEST_ON");
                windows.material.DisableKeyword("_ALPHABLEND_ON");
                windows.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                windows.material.renderQueue = -1;



            }

        }





        //vis.material.color = new Color(0.451f, 0.243f, 0.067f, 1f);
        //Debug.Log(vis.material.color);
    }




}
