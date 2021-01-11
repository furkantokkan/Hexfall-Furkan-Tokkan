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

    public static HexSelectHandler instance;

    public List<GameObject> neighboursList = new List<GameObject>();

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

    void Update()
    {
        MakeGroupOfHexes();
    }

   public void FindNeighbours(Hexagon hexToCheck)
    {
        if (GameManager.instance.selectedHex != null)
        {
            int startingHexX = hexToCheck.column; //x
            int startingHexY = hexToCheck.row; //y

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

            AddToList();

        }
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
        neighboursList.Add(upHex);
        neighboursList.Add(upLeftHex);
        neighboursList.Add(upRightHex);
        neighboursList.Add(botLeftHex);
        neighboursList.Add(botRightHex);
        neighboursList.Add(botHex);
    }

    void MakeGroupOfHexes()
    {
        if (GameManager.instance.selectedHex != null)
        {

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
                        if (GameManager.instance.selectedHexesList.Count > 0)
                        {
                            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
                        }

                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(upLeftHex);
                        GameManager.instance.selectedHexesList.Add(upHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();
                    }
                    break;
                case 2:
                    if (upHex == null || upRightHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        if (GameManager.instance.selectedHexesList.Count > 0)
                        {
                            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
                        }

                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(upHex);
                        GameManager.instance.selectedHexesList.Add(upRightHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();
                    }
                    break;
                case 3:
                    if (upRightHex == null || botRightHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        if (GameManager.instance.selectedHexesList.Count > 0)
                        {
                            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
                        }

                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(upRightHex);
                        GameManager.instance.selectedHexesList.Add(botRightHex);

                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();
                    }
                    break;
                case 4:
                    if (botHex == null || botRightHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        if (GameManager.instance.selectedHexesList.Count > 0)
                        {
                            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
                        }

                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(botRightHex);
                        GameManager.instance.selectedHexesList.Add(botHex);


                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();

                    }
                    break;
                case 5:
                    if (botLeftHex == null || botHex == null)
                    {
                        selectIndex++;
                    }
                    else
                    {
                        if (GameManager.instance.selectedHexesList.Count > 0)
                        {
                            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
                            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
                        }

                        GameManager.instance.selectedHexesList.Clear();
                        GameManager.instance.selectedHexesList.Add(GameManager.instance.selectedHex);
                        GameManager.instance.selectedHexesList.Add(botHex);
                        GameManager.instance.selectedHexesList.Add(botLeftHex);


                        GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
                        GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();

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
