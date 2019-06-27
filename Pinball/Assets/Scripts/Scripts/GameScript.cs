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
        LoadRanking(false);
    }

    void Update()
    {
        if(last < score)
        {
            Debug.Log($"Player Score: ${score}");
        }

        last = score;
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
