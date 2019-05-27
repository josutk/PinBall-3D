using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public int score
    { get; set; }

    public int multiplier
    { get; set; }

    public int last = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(last < score)
        {
            Debug.Log($"Player Score: ${score}");
        }

        last = score;
    }
}
