using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWallTrigger : MonoBehaviour
{
    public Collider trigger;

    void OnTriggerExit(Collider collider)
    {
       trigger.enabled = false; 
    }
}
