using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ColisionZoneScript : MonoBehaviour {
    public Transform spawnPosition;
    public GameObject sphere;
    //public Vector3 initPosition;
    public int lives = 3;
    public bool gameOver = false;
    public TextMesh display;
    private int saveScore;    

    void Update() {
        
        if (!GameObject.FindGameObjectWithTag("Sphere") && !gameOver) {            
            lives--;            

            sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
            if (display) {
                display.text = "Esferas: " + lives.ToString();
            }
        }
        if (lives < 0) {
            if (display) {
                display.text = "GAME OVER";
                gameOver = true;               
            }
        }
        if (gameOver) {
            if (GameObject.FindGameObjectWithTag("Sphere")) {
                Destroy(GameObject.FindGameObjectWithTag("Sphere"));                                               
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                //SceneManager.LoadScene("FGArcadeScene");
                saveScore = GameObject.Find("ScoreManager").GetComponent<ScoreManegerScript>().score;
                Debug.Log("ScoreScene " + GameObject.Find("ScoreManager").GetComponent<ScoreManegerScript>().score);
                PlayerPrefs.SetInt("Score", saveScore);
                SceneManager.LoadScene("RankingScene");
            }
            //Debug.Log("Score " + GameObject.Find("ScoreManager").GetComponent<ScoreManegerScript>().score);
        }
                
    }
}
