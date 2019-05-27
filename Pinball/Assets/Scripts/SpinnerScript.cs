using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameScript gameScript;

    private float lastFrameAngle = 180;

    private bool didTurn = false;


    public int turns
    { get; set; }

    // Component score
    private const int SCORE = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        GameObject game = GameObject.FindGameObjectWithTag("Game");

        if(game == null)
        {
            Debug.Log("Game is null!");
        }

        gameScript = game.GetComponent<GameScript>();
    }

    // There is a bug with this component. If it is turning with negative velocity, the
    // score is added when you make a half turn.

    void Update()
    {
        if(rb.angularVelocity != Vector3.zero)
        {
            if(rb.angularVelocity.x > 0 && lastFrameAngle == 180 && transform.eulerAngles.y == 0)
            {
                gameScript.score += SCORE;
            }
            else if(rb.angularVelocity.x < 0 && lastFrameAngle == 0 && transform.eulerAngles.y == 180)
            {
                gameScript.score += SCORE;
            }

            lastFrameAngle = transform.eulerAngles.y;
        }

        Debug.Log($"AnglesE = {transform.eulerAngles}");
    }
}
