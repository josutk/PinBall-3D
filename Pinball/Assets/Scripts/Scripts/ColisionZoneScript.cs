using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ColisionZoneScript : MonoBehaviour {
    public Transform spawnPosition;
    public GameObject sphere;
    //public Vector3 initPosition;
    public int lives = 3;
    private int saveScore;
    private bool deleteSphere = false;
    public GameObject[] balls = new GameObject[3];

    private bool gameOver = false;

    private GameScript game;
    private ScoreManager scoreManager;

    private void Start()
    {
        game = Finder.GetGameController();
        scoreManager = Finder.GetScoreManager();
    }

    void Update() {
        
        //TODO(Roger): Maybe use a collider instead of this?
        if (!GameObject.FindGameObjectWithTag(Constants.SPHERE_TAG) && !gameOver) {            
            lives--;            

            sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
            deleteSphere = true;

            GameObject[] inserts = Finder.GetAllExingableInserts();

            if(inserts != null)
            {
                inserts.Select(x => {
                    Material[] materials = x.GetComponent<Renderer>().materials;

                    if(materials != null)
                    {
                        materials
                        .Select(y => 
                            { 
                                y.DisableKeyword("_EMISSION"); return y; 
                            }
                        ).Count();
                    }
                    
                    Light light = x.GetComponent<Light>();

                    if(light != null)
                    {
                        x.GetComponent<Light>().enabled = false;
                    }

                    return x;
                }).Count();
            }

            scoreManager.ResetMultiplier();
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
