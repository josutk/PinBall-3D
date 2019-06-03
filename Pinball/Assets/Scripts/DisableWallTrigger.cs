using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWallTrigger : MonoBehaviour
{
    public MoveUpBehaviour wallTrigger;

    void OnTriggerExit(Collider collider)
    {
        wallTrigger.enabled = false;
    }
}
