using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    }

    void FindNeighbours()
    {
        if (GameManager.instance.selectedHex != null)
        {
            int startingHexX = GameManager.instance.selectedHex.GetComponent<Hexagon>().column; //x
            int startingHexY = GameManager.instance.selectedHex.GetComponent<Hexagon>().row; //y


            ClearHexes();
            try
            {
                upHex = GridManager.hexArray[startingHexX, startingHexY + 1];
            }
            catch
            {
                Debug.Log("UpMissing");
            }

            if (startingHexX % 2 == 0)
            {
                #region double
                try
                {
                    upRightHex = GridManager.hexArray[startingHexX + 1, startingHexY];
                }
                catch
                {
                    Debug.Log("UpRightMissing");
                }
                try
                {
                    upLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY];
                }
                catch
                {
                    Debug.Log("UpLeftMissing");
                }
                try
                {
                    botRightHex = GridManager.hexArray[startingHexX + 1, startingHexY - 1];
                }
                catch
                {
                    Debug.Log("BotRightMissing");
                }
                try
                {
                    botLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY - 1];
                }
                catch
                {
                    Debug.Log("BotLeftMissing");
                }
                #endregion
            }
            else
            {
                #region one
                try
                {
                    upRightHex = GridManager.hexArray[startingHexX + 1, startingHexY + 1];
                }
                catch
                {
                    Debug.Log("UpRightMissing");
                }
                try
                {
                    upLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY + 1];
                }
                catch
                {
                    Debug.Log("UpLeftMissing");
                }
                try
                {
                    botRightHex = GridManager.hexArray[startingHexX + 1, startingHexY];
                }
                catch
                {
                    Debug.Log("BotRightMissing");
                }
                try
                {
                    botLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY];
                }
                catch
                {
                    Debug.Log("BotLeftMissing");
                }
                #endregion
            }

            try
            {
                botHex = GridManager.hexArray[startingHexX, startingHexY - 1];
            }
            catch
            {
                Debug.Log("BotMissing");
            }



        }
    }
    void ClearHexes()
    {
        upHex = null;
        upLeftHex = null;
        upRightHex = null;
        botLeftHex = null;
        botRightHex = null;
        botHex = null;
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
                    }
                    else
                    {
                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(upLeftHex);
                        GameManager.instance.selectedHexesList.Add(upHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 2:
                    if (upHex == null || upRightHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(upHex);
                        GameManager.instance.selectedHexesList.Add(upRightHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 3:
                    if (upRightHex == null || botRightHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(upRightHex);
                        GameManager.instance.selectedHexesList.Add(botRightHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 4:
                    if (botHex == null || botRightHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(botRightHex);
                        GameManager.instance.selectedHexesList.Add(botHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().SetColor(Color.white);
                    }
                    break;
                case 5:
                    if (botLeftHex == null || botHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(botHex);
                        GameManager.instance.selectedHexesList.Add(botLeftHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().SetColor(Color.white);
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().SetColor(Color.white);
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
