using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexSelectHandler : MonoBehaviour
{
    internal GameObject upHex;
    internal GameObject upLeftHex;
    internal GameObject upRightHex;
    internal GameObject botLeftHex;
    internal GameObject botRightHex;
    internal GameObject botHex;

    private int selectIndex;

    void Update()
    {
        FindNeighbours();
        SelectGorup();
    }

    void FindNeighbours()
    {
        if (GameManager.instance.selectedTile != null)
        {
            int startingX = GameManager.instance.selectedTile.GetComponent<Hexagon>().column; //x
            int startingY = GameManager.instance.selectedTile.GetComponent<Hexagon>().row; //y

            try
            {
                upHex = GridManager.hexArray[startingX, startingY + 1];

                if (startingX % 2 == 0)
                {
                    upLeftHex = GridManager.hexArray[startingX - 1, startingY];
                    upRightHex = GridManager.hexArray[startingX + 1, startingY];
                    botLeftHex = GridManager.hexArray[startingX - 1, startingY - 1];
                    botRightHex = GridManager.hexArray[startingX + 1, startingY - 1];
                }
                else
                {
                    upLeftHex = GridManager.hexArray[startingX + 1, startingY + 1];
                    upRightHex = GridManager.hexArray[startingX - 1, startingY + 1];
                    botLeftHex = GridManager.hexArray[startingX + 1, startingY];
                    botRightHex = GridManager.hexArray[startingX - 1, startingY];
                }

                botHex = GridManager.hexArray[startingX, startingY - 1];
            }
            catch
            {
                Debug.Log("Missing Neighbours");
            }
        }
    }
    void SelectGorup()
    {
        if (GameManager.instance.selectedTile != null)
        {

        }
    }
}
