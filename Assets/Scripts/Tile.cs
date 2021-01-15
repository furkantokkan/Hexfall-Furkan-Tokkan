using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Coordinate")]
    public int tileColumn; //left right
    public int tileRow; //up down

    public void SetTileCoordinate(int x, int y)
    {
        tileColumn = x;
        tileRow = y;
    }

   
}
