using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    
    public int score;
    public ScoreManegerScript gameScore;   

    void OnCollisionEnter(Collision collision) {
        if (GameObject.FindGameObjectWithTag(Constants.SPHERE_TAG)) {
            gameScore.AddScore(score);
        }
    }
}
