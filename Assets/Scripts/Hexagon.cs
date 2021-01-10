using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public int column; //left right
    public int row; //up down

    public Color hexagonColor;
    private SpriteRenderer sr;

    private bool canMove = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Move(4, 6, 1);
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Selected");
            GameManager.instance.selectedHex = this.gameObject;
        }
    }

    public void SetCoordinate(int x, int y)
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

    IEnumerator MoveRoutine(Vector3 destination, int x, int y, float time)
    {
        Vector3 startPosition = transform.position;

        bool reached = false;

        float elapsedTime = 0f;

        canMove = false;

        while (!reached)
        {

            //movement finished 
            if (Vector3.Distance(transform.position, destination) <= 0.01f)
            {
                reached = true;
                transform.position = destination;
                SetCoordinate(x, y);
                break;
            }

            elapsedTime += Time.deltaTime;

            float lerpTime = Mathf.Clamp(elapsedTime / time, 0f, 1f);

            lerpTime = Mathf.Sin(lerpTime * Mathf.PI * 0.5f);
             
            transform.position = Vector3.Lerp(startPosition, destination, lerpTime);

            yield return null;
        }

        canMove = true;
    }
}
