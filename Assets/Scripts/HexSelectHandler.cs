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

    internal GameObject lastHex;

    public static HexSelectHandler instance;

    public List<GameObject> neighboursList = new List<GameObject>();

    private bool selected = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!selected || selectIndex >= 6)
        {
            Math.Min(selectIndex++, 5);
            MakeGroupOfHexes();
        }
    }

    public List<GameObject> FindNeighbours(int x, int y)
    {
        print(x + " " + y);
        int startingHexX = x; //x
        int startingHexY = y; //y


        neighboursList.Clear();
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
                upLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY];
            }
            catch
            {
                Debug.Log("UpLeftMissing");
            }
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

                upLeftHex = GridManager.hexArray[startingHexX - 1, startingHexY + 1];
            }
            catch
            {
                Debug.Log("UpLeftMissing");
            }
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

        AddToList();

        return neighboursList;

    }

    public void ClearHexes()
    {
        upHex = null;
        upLeftHex = null;
        upRightHex = null;
        botLeftHex = null;
        botRightHex = null;
        botHex = null;
    }
    void AddToList()
    {
        neighboursList.Add(upLeftHex);
        neighboursList.Add(upHex);
        neighboursList.Add(upRightHex);
        neighboursList.Add(botRightHex);
        neighboursList.Add(botHex);
        neighboursList.Add(botLeftHex);
    }

    public void MakeGroupOfHexes()
    {
        if (GameManager.instance.selectedHex != null)
        {
            if (lastHex != GameManager.instance.selectedHex && lastHex != null && InputManager.getInput)
            {
                selectIndex = 1;
            }

            lastHex = GameManager.instance.selectedHex;

            if (InputManager.getInput)
            {

                switch (selectIndex)
                {
                    case 1:

                        DeSelectHex();

                        if (upLeftHex == null || upHex == null)
                        {
                            selectIndex++;
                            selected = false;
                        }
                        else
                        {
                            GameManager.instance.selectedHexesList.Clear();
                            GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                            GameManager.instance.selectedHexesList.Add(upLeftHex);
                            GameManager.instance.selectedHexesList.Add(upHex);
                            selected = true;
                        }
                        break;
                    case 2:

                        DeSelectHex();

                        if (upHex == null || upRightHex == null)
                        {
                            selectIndex++;
                            selected = false;
                        }
                        else
                        {

                            GameManager.instance.selectedHexesList.Clear();
                            GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                            GameManager.instance.selectedHexesList.Add(upHex);
                            GameManager.instance.selectedHexesList.Add(upRightHex);
                            selected = true;
                        }
                        break;
                    case 3:

                        DeSelectHex();

                        if (upRightHex == null || botRightHex == null)
                        {
                            selectIndex++;
                            selected = false;
                        }
                        else
                        {

                            GameManager.instance.selectedHexesList.Clear();
                            GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                            GameManager.instance.selectedHexesList.Add(upRightHex);
                            GameManager.instance.selectedHexesList.Add(botRightHex);
                            selected = true;
                        }
                        break;
                    case 4:

                        DeSelectHex();

                        if (botHex == null || botRightHex == null)
                        {
                            selectIndex++;
                            selected = false;
                        }
                        else
                        {

                            GameManager.instance.selectedHexesList.Clear();
                            GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                            GameManager.instance.selectedHexesList.Add(botRightHex);
                            GameManager.instance.selectedHexesList.Add(botHex);
                            selected = true;
                        }
                        break;
                    case 5:

                        DeSelectHex();

                        if (botLeftHex == null || botHex == null)
                        {
                            selectIndex++;
                            selected = false;

                        }
                        else
                        {

                            GameManager.instance.selectedHexesList.Clear();
                            GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                            GameManager.instance.selectedHexesList.Add(botHex);
                            GameManager.instance.selectedHexesList.Add(botLeftHex);
                            selected = true;
                        }
                        break;
                    case 6:

                        DeSelectHex();

                        if (botLeftHex == null || upLeftHex == null)
                        {
                            selectIndex++;
                            selected = false;

                        }
                        else
                        {
                            GameManager.instance.selectedHexesList.Clear();
                            GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                            GameManager.instance.selectedHexesList.Add(botLeftHex);
                            GameManager.instance.selectedHexesList.Add(upLeftHex);
                            selected = true;
                        }
                        break;
                    default:
                        if (selectIndex > 6)
                        {
                            selectIndex = 0;
                        }
                     break;
                }


            }


        }
    }

    void DeSelectHex()
    {
        if (GameManager.instance.selectedHexesList != null && GameManager.instance.selectedHexesList.Count > 0)
        {
            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
        }
    }


}
