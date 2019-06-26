using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBehaviour : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"Displays Connected {Display.displays.Length}");
        Display.displays[1].Activate();
        Display.displays[1].SetRenderingResolution(1024, 768); 
    }
}
