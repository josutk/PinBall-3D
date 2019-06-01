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
                SceneManager.LoadScene("BoardScene");
                
            }
        }
                
    }

    //void Start() {
    //    Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
    //    //initPosition = spawnPosition.position;
    //}

    //void OnTriggerEnter(Collider other) {
    //    if (lives < 0) {
    //        gameOver = true;
    //    }
    //    if (other.gameObject.tag == "Sphere" && !gameOver) {
    //        lives--;
    //        Destroy(sphere);
    //        Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
    //        //other.GetComponent<Rigidbody>().velocity = Vector3.zero;           
    //        //other.transform.position = initPosition;
    //        LivesMangerScript.livesM = lives;
    //    }

    //}

    //void OnTriggerEnter(Collider other) {
    //    if (lives < 0) {
    //        gameOver = true;
    //    }
    //    if (other.gameObject.tag == "Sphere" && !gameOver) {
    //        Destroy(sphere);
    //        //other.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        lives--;
    //        Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);
    //        //other.transform.position = initPosition;
    //    }

    //}

    // void Update() {
    //if (lives < 0) {
    //    gameOver = true;
    //}
    //if (!GameObject.FindGameObjectWithTag("Sphere") && !gameOver) {
    //    lives--;
    //    Instantiate(sphere, spawnPosition.position, sphere.transform.rotation);

    //}
    //}

    //void OnCollisionEnter(Collision collision) {
    //   if(!GameObject.FindGameObjectWithTag("Sphere")) {
    //        collision.rigidbody.velocity = Vector3.zero;
    //        collision.transform.position = initPosition;
    //    }
    //}
}
