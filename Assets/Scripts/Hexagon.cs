using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public int column; //left right
    public int row; //up down

    public Color hexagonColor;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Selected");
            GameManager.instance.selectedTile = this.gameObject;
        }
    }

    public void SetPosition(int x, int y)
    {
        column = x;
        row = y;
    }

    public void SetColor(Color color)
    {
        hexagonColor = color;
        sr.color = hexagonColor;
    }
}
