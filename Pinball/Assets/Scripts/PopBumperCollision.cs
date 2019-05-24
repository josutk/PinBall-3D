using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopBumperCollision : MonoBehaviour
{
    private ElasticCollison elastic = new ElasticCollison();
    private int increaseVelocity = 15;
    public Color color = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, color.a);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision) {

        if (elastic.sphereColision(collision)) {

            gameObject.GetComponent<Renderer>().material.color = changeColor();
            Vector3 vector = collision.rigidbody.velocity;
            vector.x = (collision.transform.position.x - transform.position.x) * increaseVelocity;
            vector.y = (collision.transform.position.y - transform.position.y) * increaseVelocity;
            vector.z = (collision.transform.position.z - transform.position.z) * increaseVelocity;
            collision.rigidbody.velocity = vector;

        
        }
    }

    private Color changeColor() {

        Color newColor = Color.green;
        return new Color(newColor.r, newColor.g, newColor.b, newColor.a);

    }
}
