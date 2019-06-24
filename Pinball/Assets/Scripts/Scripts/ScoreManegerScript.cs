using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManegerScript : MonoBehaviour {

    public int score { get; private set; }

    public TextMesh scoreText;    
    private string scoreTextFormat = "Score: {0}";

    void Start() {
        AddScore(0);
    }

    public void AddScore(int score) {
        this.score += score;        
        UpdateScore();
    }

    void UpdateScore() {
        //scoreText.text = string.Format(scoreTextFormat, this.score);
        scoreText.text = this.score.ToString("N0").PadLeft(11, '0');        
    }
}
