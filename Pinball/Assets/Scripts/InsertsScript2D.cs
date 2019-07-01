using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertsScript2D : MonoBehaviour
{
    public GameObject insert;
    public Sprite newSprite;
    public Sprite oldSprite;
    private bool changeSprit  = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (changeSprit)
        {
            insert.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        changeSprit = false;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        changeSprit = true;
        collision.rigidbody.AddForce(-collision.contacts[0].normal * 35, ForceMode2D.Impulse);
    }
}
