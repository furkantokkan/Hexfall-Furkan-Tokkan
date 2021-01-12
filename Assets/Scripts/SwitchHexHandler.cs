﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHexHandler : MonoBehaviour
{
    public float hexSwitchSpeed = 1f;
    public float checkDelay = 0.10f;
    internal Hexagon firstHex;
    internal Hexagon secondHex;
    internal Hexagon thirdHex;

    private MatchHandler matchHandler;
    private void Awake()
    {
        matchHandler = GetComponent<MatchHandler>();
    }
    public void MoveRight()
    {
        if (GameManager.instance.selectedHexesList != null && GameManager.instance.selectedHex != null)
        {
            firstHex = GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>();
            secondHex = GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>();
            thirdHex = GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>();
            StartCoroutine(TurnRoutine());
        }

    }
    IEnumerator TurnRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            matchHandler.ClearMatch();
            firstHex.Move(secondHex.column, secondHex.row, hexSwitchSpeed);
            secondHex.Move(thirdHex.column, thirdHex.row, hexSwitchSpeed);
            thirdHex.Move(firstHex.column, firstHex.row, hexSwitchSpeed);
            yield return new WaitForSeconds(hexSwitchSpeed);
            matchHandler.AddMatch();
            if (matchHandler.stopRoutine)
            {
                matchHandler.stopRoutine = false;
                matchHandler.ClearMatch();
                ResetState();
                break;
            }
            yield return null;
        }
        ResetState();
    }

    void ResetState()
    {
        HexSelectHandler.instance.ClearHexes();
        HexSelectHandler.instance.neighboursList.Clear();
    }

}




