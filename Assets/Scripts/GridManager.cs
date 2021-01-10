using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Transform gridParent;
    public Transform hexParent;
    public GameObject positionHolder;
    public GameObject hexagon;

    public int columnsSize = 8; //left
    public int rowsSize = 9; //up 

    private GameObject lastYHex;
    private GameObject lastXHex;

    public static GameObject[,] tileArray;
    public static GameObject[,] hexArray;


    private void Awake()
    {
        GenerateTiles();
        GenerateRandomHex();
    }

    void GenerateTiles()
    {
        tileArray = new GameObject[rowsSize, rowsSize * (columnsSize - 1)];
        hexArray = new GameObject[rowsSize, rowsSize * (columnsSize - 1)];

        for (int y = 0; y < rowsSize; y++)
        {
            GameObject newYHex = Instantiate(positionHolder.gameObject, gridParent.transform.position, Quaternion.identity);
            newYHex.transform.SetParent(gridParent.transform);
            tileArray[0, y] = newYHex;
            newYHex.GetComponent<Tile>().tileRow = y;
            newYHex.name = 0 + " , " + y;

            if (y == 0)
            {
                newYHex.transform.position = new Vector3(-2.25f, -3.825f, 0);
            }
            else
            {
                newYHex.transform.position = new Vector3(lastYHex.transform.position.x, lastYHex.transform.position.y + 0.85f, 0);
            }
            lastYHex = newYHex;

            for (int x = 0; x < columnsSize - 1; x++)
            {
                GameObject newXHex = Instantiate(positionHolder.gameObject, gridParent.transform.position, Quaternion.identity);
                newXHex.transform.SetParent(gridParent);

                tileArray[x + 1, y] = newXHex;
                newXHex.name = (x + 1) + " , " + y;

                newXHex.GetComponent<Tile>().tileColumn = x + 1;
                newXHex.GetComponent<Tile>().tileRow = y;

                if (x == 0)
                {
                    newXHex.transform.position = new Vector3(lastYHex.transform.position.x + 0.75f, lastYHex.transform.position.y + 0.425f, 0);
                }
                else
                {
                    float newYtransform;

                    if (x % 2 == 0)
                    {
                        newYtransform = lastXHex.transform.position.y + 0.425f;
                    }
                    else
                    {
                        newYtransform = lastXHex.transform.position.y + -0.425f;
                    }

                    newXHex.transform.position = new Vector3(lastXHex.transform.position.x + +0.75f, newYtransform, 0);
                }

                lastXHex = newXHex;
            }
        }
    }

    private Color GetRandomColor()
    {
        int index = Random.Range(0, GameManager.instance.colors.Length);

        //added to avoid possible error
        if (GameManager.instance.colors[index] == null)
        {
            Debug.Log(index + " " + "out of array");
            return GameManager.instance.colors[0];
        }

        return GameManager.instance.colors[index];
    }

    void PlaceHexToTile(Hexagon hex, Color color, int x, int y)
    {
        hex.transform.SetParent(hexParent);

        hex.transform.position = new Vector3(tileArray[x,y].transform.position.x,
            tileArray[x, y].transform.position.y, 0);

        hex.transform.rotation = Quaternion.identity;

        hex.SetColor(color);
        hex.SetPosition(x, y);
    }

    void GenerateRandomHex()
    {
        for (int y = 0; y < rowsSize; y++)
        {
            GameObject randomHexRow = Instantiate(hexagon, gridParent.transform.position, Quaternion.identity);

            if (randomHexRow != null)
            {
                PlaceHexToTile(randomHexRow.GetComponent<Hexagon>(), GetRandomColor(), 0, y);
            }

            for (int x = 0; x < columnsSize - 1; x++)
            {
                GameObject randomHexColumn = Instantiate(hexagon, gridParent.transform.position, Quaternion.identity);

                if (randomHexColumn != null)
                {
                    PlaceHexToTile(randomHexColumn.GetComponent<Hexagon>(),GetRandomColor(),x + 1, y);
                }
            }
        }
    }
}
