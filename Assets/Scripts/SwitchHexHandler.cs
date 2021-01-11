using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchHexHandler : MonoBehaviour
{
    public float hexSwitchSpeed = 1f;
    private Hexagon firstHex;
    private Hexagon secondHex;
    private Hexagon thirdHex;
    public void MoveRight()
    {
        if (GameManager.instance.selectedHexesList != null)
        {
            firstHex = GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>();
            secondHex = GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>();
            thirdHex = GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>();
        }
        StartCoroutine(TurnRoutine());

    }
    IEnumerator TurnRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            firstHex.Move(secondHex.column, secondHex.row, hexSwitchSpeed);
            secondHex.Move(thirdHex.column, thirdHex.row, hexSwitchSpeed);
            thirdHex.Move(firstHex.column, firstHex.row, hexSwitchSpeed);
            yield return new WaitForSeconds(hexSwitchSpeed);
        }

    }
}
