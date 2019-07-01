using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopScript : MonoBehaviour
{

    private GameScript game;

    private void Start()
    {
        game = Finder.GetGameController();        
    }

    void OnCollisionEnter(Collision other)
    {
        if(CollisionHelper.DidCollideWithSphere(other.gameObject.tag))
        {
            other.gameObject.GetComponent<Renderer>().enabled = false;
            game.LoadBonusLevel(other.gameObject, other.relativeVelocity);
        }
    }
}
