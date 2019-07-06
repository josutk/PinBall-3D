using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameScript : MonoBehaviour
{
    private SignalHandlerScript signalHandler;

    public const float TILT_THRESHOLD_X = 5f;
    public const float TILT_THRESHOLD_Z = 0f;

    int velocity = 0;

    private struct SavedStatus
    {
        public List<Rigidbody> rbs;
        public Vector3 speedOfCollisionWithScoop;
        public GameObject sphereInsideScoop;
    }

    private SavedStatus savedStatus;

    void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        
        LoadMenu();
    }

    void Update()
    {
        if((IsFGArcadeLoaded || IsPinballBetLoaded) && (signalHandler.angle.angleX != 0) || (signalHandler.angle.angleZ != 0))
        {
            TiltBall(signalHandler.angle);
        }
    }

    private void TiltBall(SignalHandlerScript.Angle amount)
    {
        GameObject[] spheres = Finder.GetSpheres();

        foreach(GameObject sphere in spheres)
        {
            Rigidbody rb = sphere.GetComponent<Rigidbody>();

            //rb.velocity = new Vector3(amount.angleX, 0, amount.angleZ);
            rb.AddForce(amount.angleX, 0, amount.angleZ);
        }
    }

    private bool DidTilt
    {
        get
        {
            if (signalHandler.angle.angleX > 0 || signalHandler.angle.angleZ > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void LoadBonusLevel(GameObject sphere, Vector3 speed)
    {
        savedStatus.speedOfCollisionWithScoop = speed;
        savedStatus.sphereInsideScoop = sphere;
        EnableLevel();
        FreezeLevel();
    }

    public void UnloadBonusLevel()
    {
        DisableLevel();
        UnfreezeInputs();
        UnfreezeLevel();
        ShowBall();
        ThrowBall();
    }

    private void ShowBall()
    {
        savedStatus.sphereInsideScoop.GetComponent<Renderer>().enabled = true;
    }

    private void ThrowBall()
    {
        Vector3 oppositeSpeed = new Vector3(-savedStatus.speedOfCollisionWithScoop.x,
                                            0,
                                            -savedStatus.speedOfCollisionWithScoop.z);
        savedStatus.sphereInsideScoop.GetComponent<Rigidbody>().velocity = oppositeSpeed;
    }

    private void UnfreezeInputs()
    {
        signalHandler.freeze = false;
    }

    private void DisableLevel()
    {
        Camera backglassCamera =  Finder.GetBonusLevelBackglassCamera();
        backglassCamera.enabled = false;
        Camera fgarcadeBackglassCamera = Finder.GetFGArcadeBackglassCamera();
        fgarcadeBackglassCamera.enabled = true;

        SceneManager.UnloadSceneAsync(Constants.BONUS_LEVEL_SCENE);
    }

    private void UnfreezeLevel()
    {
        GameObject[] spheres = Finder.GetSpheres();

        Tuple<Rigidbody, Rigidbody> a = Tuple.Create(new Rigidbody(), new Rigidbody());

        var list = spheres.Zip(savedStatus.rbs, (first, second) => 
            Tuple.Create(first.GetComponent<Rigidbody>(), second));
        

        foreach(var currentAndPrevious in list)
        {
            currentAndPrevious.Item1.velocity = currentAndPrevious.Item2.velocity;
            currentAndPrevious.Item1.angularVelocity = currentAndPrevious.Item1.angularVelocity;
            currentAndPrevious.Item1.useGravity = true;
        }
    }

    private void EnableLevel()
    {
        Camera backglass = Finder.GetFGArcadeBackglassCamera();
        backglass.enabled = false;
        SceneManager.LoadScene(Constants.BONUS_LEVEL_SCENE, LoadSceneMode.Additive);
    }

    private void FreezeLevel()
    {
        FreezeSpheres();
        FreezeInputs();
    }

    private void FreezeInputs()
    {
        signalHandler.freeze = true;
    }

    private void FreezeSpheres()
    {
        List<Rigidbody> rbs = new List<Rigidbody>();
        List<Rigidbody> previous = new List<Rigidbody>();

        foreach(GameObject sphere in Finder.GetSpheres())
        {
            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            previous.Add(rb);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
        }

        savedStatus.rbs = previous;
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

        ScoreManager scoreManager = Finder.GetScoreManager();
        scoreManager.ResetScore();
        SceneManager.LoadScene(Constants.FGARCADE_SCENE, LoadSceneMode.Additive);
    }

    public void LoadPinballBet()
    {
        UnloadOtherScenes();

        ScoreManager scoreManager = Finder.GetScoreManager();
        scoreManager.ResetScore();

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
