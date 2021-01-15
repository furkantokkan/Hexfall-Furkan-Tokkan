using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHexHandler : MonoBehaviour
{
    internal Hexagon firstHex;
    internal Hexagon secondHex;
    internal Hexagon thirdHex;

    private MatchHandler matchHandler;
    private UIManager uıManager;
    private void Awake()
    {
        matchHandler = GetComponent<MatchHandler>();
        uıManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    public void MoveRight(bool moveRight)
    {
        if (GameManager.instance.selectedHexesList != null && GameManager.instance.selectedHex != null)
        {
            firstHex = GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>();
            secondHex = GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>();
            thirdHex = GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>();
            InputManager.getInput = false;
            StartCoroutine(TurnRoutine(moveRight));
        }

    }

    IEnumerator TurnRoutine(bool moveRight)
    {
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.canHexTakeNewPlace = false;
            matchHandler.ClearMatch();

            if (matchHandler.stopRoutine)
            {
                InputManager.getInput = true;
                matchHandler.stopRoutine = false;
                matchHandler.ClearMatch();
                ResetState();
                GameManager.instance.moves++;
                uıManager.UpdateMovesText();
                yield return new WaitForSeconds(0.33f);
                GameManager.instance.canHexTakeNewPlace = true;
                break;
            }

            if (moveRight)
            {
                if (firstHex != null)
                {
                    firstHex.Move(secondHex.column, secondHex.row, GameManager.instance.hexSwitchSpeed);
                }
                if (secondHex != null)
                {
                    secondHex.Move(thirdHex.column, thirdHex.row, GameManager.instance.hexSwitchSpeed);
                }
                if (thirdHex != null)
                {
                    thirdHex.Move(firstHex.column, firstHex.row, GameManager.instance.hexSwitchSpeed);
                }
            }
            else
            {
                if (firstHex != null)
                {
                    firstHex.Move(thirdHex.column, thirdHex.row, GameManager.instance.hexSwitchSpeed);
                }
                if (secondHex != null)
                {
                    secondHex.Move(firstHex.column, firstHex.row, GameManager.instance.hexSwitchSpeed);
                }
                if (thirdHex != null)
                {
                    thirdHex.Move(secondHex.column, secondHex.row, GameManager.instance.hexSwitchSpeed);
                }
            }
            
            yield return new WaitForSeconds(GameManager.instance.hexSwitchSpeed);
            matchHandler.AddMatch();
            if (i == 2)
            {
                GameManager.instance.moves++;
                uıManager.UpdateMovesText();
                GameManager.instance.canHexTakeNewPlace = true;
                InputManager.getInput = true;
            }
            ResetState();
            yield return null;
        }
    }

    void ResetState()
    {
        HexSelectHandler.instance.ClearHexes();
        HexSelectHandler.instance.neighboursList.Clear();
    }

}




