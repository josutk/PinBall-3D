using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int score { get; private set; }

    private int scoreToIncreaseMultiplier = 10000;

    private int multiplierIncreaseAmount = 10000;

    public int multiplier = 1;
 
    public TextMesh scoreText;    

    void Start() {
        AddScore(0);
    }

    public void AddScore(int score) {
        this.score += (score * multiplier);   
        UpdateScore();
    }

    private void Update()
    {
        if(score > scoreToIncreaseMultiplier)
        {
            multiplier++;
            scoreToIncreaseMultiplier += multiplierIncreaseAmount;
        }
    }

    void UpdateScore() 
    {        
        scoreText.text = this.score.ToString("N0").PadLeft(11, '0');        
    }

    public void ResetScore()
    {
        score = 0;
        ResetMultiplier();
        UpdateScore();
    }

    public void ResetMultiplier() => multiplier = 1;
}
