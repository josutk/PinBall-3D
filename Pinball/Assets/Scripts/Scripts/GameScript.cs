using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    private SignalHandlerScript signalHandler;

    public const float TILT_THRESHOLD_X = 5f;
    public const float TILT_THRESHOLD_Z = 2f;

    void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        
        LoadMenu();
    }

    void Update()
    {
        if(IsFGArcadeLoaded || IsPinballBetLoaded)
        {
            if(signalHandler.usingMSP && DidTilt)
            {
                TiltBall(signalHandler.angle.angle);    
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    Random.InitState((int)Time.time);

                    int rand = Random.Range(-2, 2);

                    TiltBall(rand);
                }
            }
        }
    }

    private void TiltBall(int amount)
    {
        GameObject[] spheres = Finder.GetSpheres();

        foreach(GameObject sphere in spheres)
        {
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            rb.AddForce(amount * TILT_THRESHOLD_X, 0, TILT_THRESHOLD_Z);
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

    public void LoadRanking(bool disableOthers = true)
    {
        if(disableOthers)
        {
            UnloadOtherScenes();
        }

        SceneManager.LoadScene(Constants.RANKING_SCENE, LoadSceneMode.Additive);
    } 

    public bool IsRankingLoaded
    {
        get
        {
            int numberOfScenes = SceneManager.sceneCount;

            for(int i = 0; i < numberOfScenes; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(scene.isLoaded && scene.name.Equals(Constants.RANKING_SCENE))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public bool IsMenuLoaded 
    {
        get
        {
            int numberOfScenes = SceneManager.sceneCount;

            for(int i = 0; i < numberOfScenes; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(scene.isLoaded && scene.name.Equals(Constants.MENU_SCENE_NAME))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public bool IsFGArcadeLoaded 
    {
        get
        {
            int numberOfScenes = SceneManager.sceneCount;

            for(int i = 0; i < numberOfScenes; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(scene.isLoaded && scene.name.Equals(Constants.FGARCADE_SCENE))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public bool IsPinballBetLoaded
    {
        get
        {
            int numberOfScenes = SceneManager.sceneCount;

            for(int i = 0; i < numberOfScenes; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(scene.isLoaded && scene.name.Equals(Constants.PINBALLBET_SCENE))
                {
                    return true;
                }
            }

            return false;
        }   
    }
}
