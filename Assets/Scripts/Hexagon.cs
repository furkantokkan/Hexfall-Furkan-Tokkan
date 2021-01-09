using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public int column; //left right
    public int row; //up down

   
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Selected");
            GameManager.instance.selectedTile = this.gameObject;
        }
    }
}
