using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManegerScript : MonoBehaviour {
    
    public int score { get; private set; }

    public TextMesh scoreText;
    private string scoreTextFormat = "Score: {0}";

    //public TextMesh scoreEndText;
    //public string scoreEndTextFormat = "Final Score: {0}";

    void Start() {
        AddScore(0);
    }

    public void AddScore(int score) {
        this.score += score;
        UpdateScore();
    }

    //public void ClearScore() {
    //    score = 0;
    //    UpdateScore();
    //}

    void UpdateScore() {
        scoreText.text = string.Format(scoreTextFormat, this.score);
        // scoreEndText.text = string.Format(scoreEndTextFormat, this.score);
    }
}
