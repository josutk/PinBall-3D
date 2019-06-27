using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    private SignalHandlerScript signalHandler;

    public const float TILT_THRESHOLD = 5;

    void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        
        LoadMenu();
        LoadRanking(false);      
    }

    void Update()
    {
        TiltBall();    
    }

    private void TiltBall()
    {
        if(DidTilt)
        {
            GameObject[] spheres = Finder.GetSpheres();

            foreach(GameObject sphere in spheres)
            {
                // Rigidbody rb = sphere.GetComponent<Rigidbody>();
                // rb.AddForce();
            }
        }
    }

    private bool DidTilt
    {
        get
        {
            if (signalHandler.angle.angle > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private void UnloadOtherScenes()
    {
        int numberOfScenes = SceneManager.sceneCount;

        for(int i = 0; i < numberOfScenes; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if(scene.isLoaded && !scene.name.Equals(Constants.PERSISTANT_SCENE_NAME))
            {
                SceneManager.UnloadSceneAsync(scene.name);
            }
        }
    }

    public void LoadMenu()
    {
        UnloadOtherScenes();
        SceneManager.LoadScene(Constants.MENU_SCENE_NAME, LoadSceneMode.Additive);
    }

    public void LoadFGArcade()
    {
        UnloadOtherScenes();
        SceneManager.LoadScene(Constants.FGARCADE_SCENE, LoadSceneMode.Additive);
    }

    public void LoadPinballBet()
    {
        UnloadOtherScenes();
        SceneManager.LoadScene(Constants.PINBALLBET_SCENE, LoadSceneMode.Additive);
    }

    public void LoadRanking(bool unloadOtherScenes = true)
    {
        if(unloadOtherScenes) UnloadOtherScenes();
        SceneManager.LoadScene(Constants.RANKING_SCENE, LoadSceneMode.Additive);
    } 
}
