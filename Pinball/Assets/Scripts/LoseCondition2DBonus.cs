using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition2DBonus : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D objectCollision)
    {
       if(CollisionHelper.DidCollideWith2DSpere(objectCollision.gameObject.tag))
        {
            Debug.Log("Voce Perdeu");
        }
    }
}
