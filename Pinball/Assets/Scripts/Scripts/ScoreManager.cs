using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int score { get; private set; }

    public TextMesh scoreText;    

    void Start() {
        AddScore(0);
    }

    public void AddScore(int score) {
        this.score += score;        
        UpdateScore();
    }

    void UpdateScore() {        
        scoreText.text = this.score.ToString("N0").PadLeft(11, '0');        
    }
}
