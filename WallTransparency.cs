using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{

    GameObject g;
    Renderer vis;

    float visR;
    float visG;
    float visB;

    
   
    void Start()
    {
        vis = GetComponent<Renderer>();
        Debug.Log(vis.material.color);

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

        vis.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);                 //////changes render mode to transparent
        vis.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        vis.material.SetInt("_ZWrite", 0);
        vis.material.DisableKeyword("_ALPHATEST_ON");
        vis.material.DisableKeyword("_ALPHABLEND_ON");
        vis.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        vis.material.renderQueue = 3000;



        vis.material.color = new Color(visR, visG, visB, 0.5f); // MAKES THE MATERIAL TRANSPARENT AND USES THE ORIGINAL COLOR

    }   




    private void OnTriggerExit(Collider other)
    {
        //vis.enabled = true;

        vis.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);             ////////// CHANGES RENDER MORE TO OPAQUE
        vis.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        vis.material.SetInt("_ZWrite", 1);
        vis.material.DisableKeyword("_ALPHATEST_ON");
        vis.material.DisableKeyword("_ALPHABLEND_ON");
        vis.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        vis.material.renderQueue = -1;



        //vis.material.color = new Color(0.451f, 0.243f, 0.067f, 1f);
        //Debug.Log(vis.material.color);
    }




}
