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
    public SpriteRenderer pinballBetPreview;
    public static bool isFromMenu = false;

    void Start()
    {
        signalHandler = Finder.GetSignalHandler();
        game = Finder.GetGameController();
        
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
            if (IsHoveringRanking) {
                game.LoadRanking();
                isFromMenu = true;

            }// Show ranking
            else if (IsHoveringFGArcade) game.LoadFGArcade();
            else if (IsHoveringPinballBet) game.LoadPinballBet();
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
            pinballBetPreview.enabled = true;
        }
        else
        {
            pinballBetPreview.enabled = false;
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
                transform.position = new Vector3(0.32f, -0.3f, -29.75f);
                break;
            case 2:
                transform.position = new Vector3(0.32f, -1.3f, -29.75f);
                break;
            case 3:
                transform.position = new Vector3(0.32f, -2.05f, -29.75f);
                break;
            default:
                break;       
        }
    }
}
