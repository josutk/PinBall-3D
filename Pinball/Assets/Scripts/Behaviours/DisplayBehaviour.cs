using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayBehaviour : MonoBehaviour
{
    void Start()
    {       
        if(IsSecondScreenConected) ActivateSmallScreen();
    }

    private void ActivateSmallScreen()
    {
        Display.displays[1].Activate();
        Display.displays[1].SetRenderingResolution(1024, 768); 
    }

    private bool IsSecondScreenConected
    {
        get
        {
            if (Display.displays.Length > 1) return true;
            else return false;
        }
    }
}
