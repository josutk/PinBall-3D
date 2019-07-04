using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour {
    
    public int score;
    private ScoreManager scoreManager;   

    private void Start()
    {
        scoreManager = Finder.GetScoreManager();
    }

    void OnCollisionEnter(Collision collision) {
        if (GameObject.FindGameObjectWithTag(Constants.SPHERE_TAG)) {
            scoreManager.AddScore(score);
        }
    }
}
