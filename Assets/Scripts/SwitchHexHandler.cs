using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHexHandler : MonoBehaviour
{
    public float hexSwitchSpeed = 1f;
    internal Hexagon firstHex;
    internal Hexagon secondHex;
    internal Hexagon thirdHex;

    private MatchHandler matchHandler;
    private GridManager gridManager;
    private UIManager uıManager;
    private void Awake()
    {
        matchHandler = GetComponent<MatchHandler>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        uıManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    public void MoveRight()
    {
        if (GameManager.instance.selectedHexesList != null && GameManager.instance.selectedHex != null)
        {
            firstHex = GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>();
            secondHex = GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>();
            thirdHex = GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>();
            InputManager.getInput = false;
            StopCoroutine(TurnRoutine());
            StartCoroutine(TurnRoutine());
        }

    }

    IEnumerator TurnRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            GameManager.instance.canHexTakeNewPlace = false;
            matchHandler.ClearMatch();
            if (firstHex != null)
            {
                firstHex.Move(secondHex.column, secondHex.row, hexSwitchSpeed);
            }
            if (secondHex != null)
            {
                secondHex.Move(thirdHex.column, thirdHex.row, hexSwitchSpeed);
            }
            if (thirdHex != null)
            {
                thirdHex.Move(firstHex.column, firstHex.row, hexSwitchSpeed);
            }
            yield return new WaitForSeconds(hexSwitchSpeed);
            matchHandler.AddMatch();
            if (MatchHandler.stopRoutine)
            {
                InputManager.getInput = true;
                MatchHandler.stopRoutine = false;
                matchHandler.ClearMatch();
                ResetState();
                GameManager.instance.moves++;
                uıManager.UpdateMovesText();
                yield return new WaitForSeconds(0.033f);
                GameManager.instance.canHexTakeNewPlace = true;
                break;
            }
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




