using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{

    private SignalHandlerScript signalHandler;
    private LinkedList<int> LLPosition = new LinkedList<int>(new int[3] {1, 2, 3});
    private LinkedListNode<int> currentPosition;

    private GameScript game;

    public SpriteRenderer fgarcadePreview;

    void Start()
    {
        signalHandler = FinderHelper.GetSignalHandler();
        game = FinderHelper.GetGameController();
        
        currentPosition = LLPosition.First;
    }

    void Update()
    {
        if (signalHandler.buttons.rightButton)
        {
            MoveUp();
        }

        if (signalHandler.buttons.leftButton)
        {
            MoveDown();
        }

        ShowInHover();

        if (signalHandler.buttons.select)
        {
            if (IsHoveringRanking) { }// Show ranking
            else if (IsHoveringFGArcade) game.LoadFGArcade();
        }

        MoveSelector();
    }

    private void ShowInHover()
    {
        if (IsHoveringFGArcade)
        {
            fgarcadePreview.enabled = true;
        }
        else
        {
            fgarcadePreview.enabled = false;
        }

        if (IsHoveringRanking)
        {
            // Show something in big screen;
        }
        else
        {
            // Don't Show
        }

        if (IsHoveringPinballBet)
        {
            // Show Something in big screen
        }
        else
        {
            // Don't Show
        }
    }

    private bool IsHoveringRanking
    {
        get
        {
            if (currentPosition.Value == 1) return true;
            else return false;
        }
    }

    private bool IsHoveringFGArcade
    {
        get
        {
            if(currentPosition.Value == 2) return true;
            else return false;
        }
    }

    private bool IsHoveringPinballBet
    {
        get
        {
            if(currentPosition.Value == 3) return true;
            else return false;
        }
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
