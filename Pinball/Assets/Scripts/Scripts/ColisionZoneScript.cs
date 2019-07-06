using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ColisionZoneScript : MonoBehaviour {
    public Transform spawnPosition;
    public GameObject sphere;
    //public Vector3 initPosition;
    public int lives = 20;
    public bool gameOver = false;
    private int saveScore;
    private bool deleteSphere = false;
    public GameObject[] balls = new GameObject[3];

    private GameScript game;

    private void Start()
    {
        game = Finder.GetGameController();
    }

    void Update() {
        
        if (!GameObject.FindGameObjectWithTag("Sphere") && !gameOver) {            
            lives--;            

            sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
            deleteSphere = true;

        }
        if (lives < 0) {
            gameOver = true;               
        }

        if (deleteSphere && !gameOver)
        {
            Destroy(balls[lives]);
        }

        deleteSphere = false;

        if (gameOver) {
            if (GameObject.FindGameObjectWithTag("Sphere")) {
                Destroy(GameObject.FindGameObjectWithTag("Sphere"));                                               
            }
            
            saveScore = Finder.GetScoreManager().score;
            
            PlayerPrefs.SetInt("Score", saveScore);
            
            game.LoadRanking();
        }
                
    }
}
