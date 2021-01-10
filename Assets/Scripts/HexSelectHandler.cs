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

    public static int selectIndex = 0;

    private GameObject lastHex;
    void Update()
    {
        FindNeighbours();
        MakeGroupOfHexes();
        print(selectIndex);
    }

    void FindNeighbours()
    {
        if (GameManager.instance.selectedHex != null)
        {
            int startingHexX = GameManager.instance.selectedHex.GetComponent<Hexagon>().column; //x
            int startingHexY = GameManager.instance.selectedHex.GetComponent<Hexagon>().row; //y

            try
            {
                upHex = GridManager.hexArray[startingHexX, startingHexY + 1];

                if (startingHexX % 2 == 0)
                {
                    upLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY];
                    upRightHex = GridManager.hexArray[startingHexX + 1, startingHexY];
                    botLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY - 1];
                    botRightHex = GridManager.hexArray[startingHexX + 1, startingHexY - 1];
                }
                else
                {
                    upLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY + 1];
                    upRightHex = GridManager.hexArray[startingHexX + 1, startingHexY + 1];
                    botLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY];
                    botRightHex = GridManager.hexArray[startingHexX + 1, startingHexY];
                }

                botHex = GridManager.hexArray[startingHexX, startingHexY - 1];
            }
            catch
            {

                Debug.Log("Missing Neighbours");
            }
        }
    }

    void MakeGroupOfHexes()
    {
        if (GameManager.instance.selectedHex != null)
        {
            GameManager.instance.selectedHex.GetComponent<Hexagon>().SetColor(Color.red);
            if (lastHex != GameManager.instance.selectedHex && lastHex != null)
            {
                selectIndex = 1;
            }

            lastHex = GameManager.instance.selectedHex;

            switch (selectIndex)
            {
                case 1:
                    if (upLeftHex == null || upHex == null)
                    {
                        selectIndex++;
                        GameManager.instance.selectedHexes[1] = null;
                        GameManager.instance.selectedHexes[2] = null;
                    }
                    else
                    {
                        GameManager.instance.selectedHexes[0] = GameManager.instance.selectedHex;
                        GameManager.instance.selectedHexes[1] = upLeftHex;
                        GameManager.instance.selectedHexes[2] = upHex;

                        GameManager.instance.selectedHexes[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 2:
                    if (upHex == null || upRightHex == null)
                    {
                        selectIndex++;
                        GameManager.instance.selectedHexes[1] = null;
                        GameManager.instance.selectedHexes[2] = null;
                    }
                    else
                    {
                        GameManager.instance.selectedHexes[0] = GameManager.instance.selectedHex;
                        GameManager.instance.selectedHexes[1] = upHex;
                        GameManager.instance.selectedHexes[2] = upRightHex;

                        GameManager.instance.selectedHexes[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 3:
                    if (upRightHex == null || botRightHex == null)
                    {
                        selectIndex++;
                        GameManager.instance.selectedHexes[1] = null;
                        GameManager.instance.selectedHexes[2] = null;
                    }
                    else
                    {
                        GameManager.instance.selectedHexes[0] = GameManager.instance.selectedHex;
                        GameManager.instance.selectedHexes[1] = upRightHex;
                        GameManager.instance.selectedHexes[2] = botRightHex;

                        GameManager.instance.selectedHexes[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 4:
                    if (botHex == null || botLeftHex == null)
                    {
                        selectIndex++;
                        GameManager.instance.selectedHexes[1] = null;
                        GameManager.instance.selectedHexes[2] = null;
                    }
                    else
                    {
                        GameManager.instance.selectedHexes[0] = GameManager.instance.selectedHex;
                        GameManager.instance.selectedHexes[1] = botRightHex;
                        GameManager.instance.selectedHexes[2] = botHex;

                        GameManager.instance.selectedHexes[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 5:
                    if (botLeftHex == null || upLeftHex == null)
                    {
                        selectIndex++;
                        GameManager.instance.selectedHexes[1] = null;
                        GameManager.instance.selectedHexes[2] = null;
                    }
                    else
                    {
                        GameManager.instance.selectedHexes[0] = GameManager.instance.selectedHex;
                        GameManager.instance.selectedHexes[1] = botHex;
                        GameManager.instance.selectedHexes[2] = botLeftHex;

                        GameManager.instance.selectedHexes[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexes[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                default:
                    if (selectIndex >= 6)
                    {
                        selectIndex = 1;
                    }
                    break;
            }
        }
    }


}
