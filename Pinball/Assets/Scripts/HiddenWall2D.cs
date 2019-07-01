using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWall2D : MonoBehaviour
{
    private SpriteRenderer sprite;
    private PolygonCollider2D wall;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        wall = GetComponent<PolygonCollider2D>();
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Opta!");
        if(CollisionHelper.DidCollideWith2DSpere(other.gameObject.tag))
        {
            Debug.Log("Here!");
            sprite.enabled = true;
            wall.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
