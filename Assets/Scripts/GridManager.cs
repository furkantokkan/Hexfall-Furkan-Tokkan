using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Transform tileMap;
    public GameObject hexagon;

    public int columnsSize = 8; //left
    public int rowsSize = 9; //up 

    private GameObject lastYHex;
    private GameObject lastXHex;

    public static GameObject[,] hexArray;
    private void Awake()
    {
        GenerateHexes();
    }

    void GenerateHexes()
    {
        hexArray = new GameObject[rowsSize, rowsSize * columnsSize - 1];

        for (int y = 0; y < rowsSize; y++)
        {
            GameObject newYHex = Instantiate(hexagon.gameObject, tileMap.transform.position, Quaternion.identity);
            newYHex.transform.SetParent(tileMap.transform);
            hexArray[0, y] = newYHex;
            newYHex.GetComponent<Hexagon>().row = y;

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
                GameObject newXHex = Instantiate(hexagon.gameObject, tileMap.transform.position, Quaternion.identity);
                newXHex.transform.SetParent(tileMap);

                hexArray[x + 1, y] = newXHex; 
   
                newXHex.GetComponent<Hexagon>().column = x + 1;
                newXHex.GetComponent<Hexagon>().row = y;

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
}
