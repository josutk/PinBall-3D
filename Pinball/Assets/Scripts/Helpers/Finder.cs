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
}