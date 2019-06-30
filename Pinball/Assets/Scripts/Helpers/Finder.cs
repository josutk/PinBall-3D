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
}