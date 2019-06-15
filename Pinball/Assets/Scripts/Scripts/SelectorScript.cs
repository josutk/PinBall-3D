using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{

    private SignalHandlerScript signalHandler;
    private LinkedList<int> LLPosition = new LinkedList<int>(new int[3] {1, 2, 3});
    private LinkedListNode<int> currentPosition;

    private GameScript game;

    void Start()
    {
        signalHandler = FinderHelper.GetSignalHandler();
        game = FinderHelper.GetGameController();
        
        currentPosition = LLPosition.First;
    }

    void Update()
    {
        if(signalHandler.buttons.rightButton)
        {
            MoveUp();
        }
        
        if(signalHandler.buttons.leftButton)
        {
            MoveDown();
        }

        if(signalHandler.buttons.select)
        {
            if(currentPosition.Value == 1) game.LoadMenu();
            else if(currentPosition.Value == 2) game.LoadFGArcade();
        }

        MoveSelector();
    }

    void MoveDown() => currentPosition = currentPosition.Previous ?? LLPosition.Last;

    void MoveUp() => currentPosition = currentPosition.Next ?? LLPosition.First;

    void MoveSelector()
    {
        switch(currentPosition.Value)
        {
            case 1:
                transform.position = new Vector3(0.34f, -0.79f, -30.21f);
                break;
            case 2:
                transform.position = new Vector3(0.34f, -1.71f, -30.21f);
                break;
            case 3:
                transform.position = new Vector3(0.34f, -2.54f, -30.21f);
                break;
            default:
                break;       
        }
    }
}
