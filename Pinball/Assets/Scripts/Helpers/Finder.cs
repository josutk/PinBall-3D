using System;
using System.Linq;
using UnityEngine;

public static class Finder
{
    public static GameScript GetGameController()
    {
        return GameObject.FindGameObjectWithTag(Constants.GAME_CONTROLLER_TAG).GetComponent<GameScript>();
    }

    public static SignalHandlerScript GetSignalHandler()
    {
        return GameObject.FindGameObjectWithTag(Constants.SIGNAL_HANDLER_TAG).GetComponent<SignalHandlerScript>();
    }

    public static ScoreManager GetScoreManager()
    {
        return GameObject.FindGameObjectWithTag(Constants.SCORE_MANAGER_TAG).GetComponent<ScoreManager>();
    }

    public static GameObject[] GetSpheres()
    {
        return GameObject.FindGameObjectsWithTag(Constants.SPHERE_TAG);
    }

    public static GameObject Get2DBall()
    {
        return GameObject.FindGameObjectWithTag(Constants.BALL_2D_TAG);
    }

    public static Camera GetFGArcadeBackglassCamera() => GameObject.FindGameObjectWithTag(Constants.FGARCADE_BACKGLASS_CAMERA).GetComponent<Camera>();

    public static Camera GetBonusLevelBackglassCamera() => 
        GameObject
            .FindGameObjectWithTag(Constants.BONUS_LEVEL_BACKGLASS_CAMERA)
            .GetComponent<Camera>();

    public static GameObject[] GetAllExingableInserts()
    {
        Transform[] allInserts = GameObject
            .FindGameObjectWithTag(Constants.INSERTS_GROUP_TAG)
            .GetComponentsInChildren<Transform>();

        GameObject[] extingableInserts = 
            allInserts
            .Select(x => x.gameObject)
            .Where(x => x.gameObject.layer == Constants.Layers.EXINGABLE_INSERTS)
            .ToArray();

        return extingableInserts;
    }
}