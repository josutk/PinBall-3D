using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public int score
    { get; set; }

    public int multiplier
    { get; set; }

    public int last = 0;

    void Start()
    {
        LoadMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(last < score)
        {
            Debug.Log($"Player Score: ${score}");
        }

        last = score;

        if(Input.GetAxis(Constants.LEFT_FLIPPER_INPUT) == 1)
        {

            Debug.Log("Input: A!");

            int numberOfScenes = SceneManager.sceneCount;

            for(int i = 0; i < numberOfScenes; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if(scene.isLoaded && !scene.name.Equals(Constants.PERSISTANT_SCENE_NAME))
                {
                    SceneManager.UnloadSceneAsync(scene.name);
                }
            }

            LoadLevelOne();
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(Constants.MENU_SCENE_NAME, LoadSceneMode.Additive);
    }

    private void LoadLevelOne()
    {
        SceneManager.LoadScene(Constants.LEVEL_ONE_SCENE_NAME, LoadSceneMode.Additive);
    }
}
