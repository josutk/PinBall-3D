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

    void Update() {
        
        if (!GameObject.FindGameObjectWithTag("Sphere") && !gameOver) {            
            lives--;            
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
                SceneManager.LoadScene("FGArcadeScene");
                
            }
        }
                
    }
}
