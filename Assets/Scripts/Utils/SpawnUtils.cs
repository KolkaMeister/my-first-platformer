using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUtils 
{


    public static Canvas canvas;

    public static void  CreateWindow<TWindowType>(TWindowType window,out TWindowType instance) where TWindowType : Object
    {
        if (canvas == null)canvas=GameObject.FindGameObjectWithTag("WindowsCreator").GetComponent<Canvas>();
        instance = Object.Instantiate(window, canvas.gameObject.transform);
    }
    
}
