using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition2DBonus : MonoBehaviour
{
    private GameScript game;

    private void Start()
    {
        game = Finder.GetGameController();
    }
    void OnCollisionEnter2D(Collision2D objectCollision)
    {
       if(CollisionHelper.DidCollideWith2DSpere(objectCollision.gameObject.tag))
        {
            game.UnloadBonusLevel();
        }
    }
}
