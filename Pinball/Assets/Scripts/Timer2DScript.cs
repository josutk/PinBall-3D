using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer2DScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Text timerDisplayed;
    float timer = 0.0f;

    void Start()
    {
        timerDisplayed = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)( timer % 60);
        timerDisplayed.text = seconds.ToString();
        if(seconds >= 21)
        {
            //Call next scene.
            timer = 0.0f;
        }
            
    }
}
