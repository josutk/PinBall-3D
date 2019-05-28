using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject sphere;
    public Transform spawnPosition;
    public int lives = 3;
    public bool gameOver = false;

 
    // Update is called once per frame
    void Update() {
        if(lives < 0){
            gameOver = true;
        }
        if(!GameObject.FindGameObjectWithTag("Sphere") && !gameOver) {
            lives--;
            Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
            
        }
        //if (gameOver) {
        //    if (Input.GetKeyDown(KeyCode.Return)) {
                
        //        SceneManager. (Application.loadedLevel);
        //        Application.LoadLevel(Application.loadedLevel);
        //    }

        //}
    }
}
