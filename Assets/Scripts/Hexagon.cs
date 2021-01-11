using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Hexagon : MonoBehaviour
{
    public int column; //left right
    public int row; //up down

    public Color hexagonColor;
    private SpriteRenderer sr;

    private bool canMove = true;

    [SerializeField] internal UnityEvent onSelected;
    [SerializeField] internal UnityEvent onDeselected;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetHexCoordinate(int x, int y)
    {
        column = x;
        row = y;
    }

    public void SetColor(Color color)
    {
        hexagonColor = color;
        sr.color = hexagonColor;
    }

    public void Move(int x, int y, float time)
    {
        if (canMove)
        {
            StartCoroutine(MoveRoutine(new Vector3(GridManager.tileArray[x, y].transform.position.x,
                  GridManager.tileArray[x, y].transform.position.y,
                  0),
                  x,
                  y,
                  time));
        }
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveRoutine(Vector3 destination, int x, int y, float time)
    {
        Vector3 startPosition = transform.position;

        bool reached = false;

        float elapsedTime = 0f;

        canMove = false;
        InputManager.getInput = false;

        while (!reached)
        {

            //movement finished 
            if (Vector3.Distance(transform.position, destination) <= 0.01f)
            {
                reached = true;
                transform.position = destination;
                SetHexCoordinate(x, y);
                break;
            }

            elapsedTime += Time.deltaTime;

            float lerpTime = Mathf.Clamp(elapsedTime / time, 0f, 1f);

            lerpTime = Mathf.Sin(lerpTime * Mathf.PI * 0.5f);
             
            transform.position = Vector3.Lerp(startPosition, destination, lerpTime);

            yield return null;
        }

        canMove = true;
        InputManager.getInput = true;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.selectedHex = this.gameObject;
            if (InputManager.getInput)
            {
                HexSelectHandler.instance.FindNeighbours(this);
            }
            HexSelectHandler.selectIndex++;
        }
    }
}
