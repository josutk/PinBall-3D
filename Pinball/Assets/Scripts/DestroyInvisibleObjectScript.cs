using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInvisibleObjectScript : MonoBehaviour
{
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }


}
