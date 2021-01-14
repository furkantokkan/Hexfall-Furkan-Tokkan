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

    internal bool canMove = true;
    internal bool reached;

    [SerializeField] internal UnityEvent onSelected;
    [SerializeField] internal UnityEvent onDeselected;

    internal GameObject upHex;
    internal GameObject upLeftHex;
    internal GameObject upRightHex;
    internal GameObject botLeftHex;
    internal GameObject botRightHex;
    internal GameObject botHex;

    internal List<GameObject> neighbours = new List<GameObject>();
    internal List<GameObject> matchedNeighbours = new List<GameObject>();

    private GridManager gridManager;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        GameManager.instance.allHexesList.Add(this.gameObject);
    }
    private void Start()
    {
        neighbours.AddRange(HexSelectHandler.instance.FindNeighbours(column, row));
        AddHexes();
        CheckMatchedNeighbours();
    }
    public void SetHexCoordinate(int x, int y)
    {
        GridManager.hexArray[x, y] = this.gameObject;
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
            try
            {
                GridManager.hexArray[column, row] = null;

                StartCoroutine(MoveRoutine(new Vector3(GridManager.tileArray[x, y].transform.position.x,
                  GridManager.tileArray[x, y].transform.position.y,
                   0),
                   x,
                   y,
                  time));

            }
            catch
            {
                Debug.Log("Stoped");
            }

        }
    }
    private void OnDisable()
    {
        GameManager.instance.allHexesList.Remove(this.gameObject);
        if (GameManager.instance.selectedHexesList != null && GameManager.instance.selectedHexesList.Count > 0)
        {
            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
        }

        GameManager.instance.selectedHexesList.Clear();

    }
    IEnumerator MoveRoutine(Vector3 destination, int x, int y, float time)
    {

        Vector3 startPosition = transform.position;

         reached = false;

        float elapsedTime = 0f;

        canMove = false;

        if (GameManager.instance.selectedHexesList.Count > 0 && GameManager.instance.selectedHexesList != null)
        {
            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
        }
        while (!reached)
        {
            //movement finished 
            if (Vector3.Distance(transform.position, destination) <= 0.05f )
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
        ClearHexes();
        neighbours.Clear();
        matchedNeighbours.Clear();
        yield return new WaitForSeconds(0.1f);
        canMove = true;
        neighbours.AddRange(HexSelectHandler.instance.FindNeighbours(x, y));
        AddHexes();
        CheckMatchedNeighbours();
    }

    void CheckMatchedNeighbours()
    {

        if (upLeftHex != null && upHex != null)
        {
            if (hexagonColor == upLeftHex.GetComponent<Hexagon>().hexagonColor && hexagonColor == upHex.GetComponent<Hexagon>().hexagonColor)
            {
                if (!matchedNeighbours.Contains(gameObject))
                {
                    matchedNeighbours.Add(gameObject);
                }
                if (!matchedNeighbours.Contains(upLeftHex))
                {
                    matchedNeighbours.Add(upLeftHex);
                }
                if (!matchedNeighbours.Contains(upHex))
                {
                    matchedNeighbours.Add(upHex);
                }
            }
        }
        if (upHex != null && upRightHex != null)
        {
            if (hexagonColor == upHex.GetComponent<Hexagon>().hexagonColor && hexagonColor == upRightHex.GetComponent<Hexagon>().hexagonColor)
            {
                if (!matchedNeighbours.Contains(gameObject))
                {
                    matchedNeighbours.Add(gameObject);
                }
                if (!matchedNeighbours.Contains(upHex))
                {
                    matchedNeighbours.Add(upHex);
                }
                if (!matchedNeighbours.Contains(upRightHex))
                {
                    matchedNeighbours.Add(upRightHex);
                }
            }
        }
        if (upRightHex != null && botRightHex != null)
        {
            if (hexagonColor == upRightHex.GetComponent<Hexagon>().hexagonColor && hexagonColor == botRightHex.GetComponent<Hexagon>().hexagonColor)
            {
                if (!matchedNeighbours.Contains(gameObject))
                {
                    matchedNeighbours.Add(gameObject);
                }
                if (!matchedNeighbours.Contains(upRightHex))
                {
                    matchedNeighbours.Add(upRightHex);
                }
                if (!matchedNeighbours.Contains(botRightHex))
                {
                    matchedNeighbours.Add(botRightHex);
                }
            }
        }
        if (botRightHex != null && botHex != null)
        {
            if (hexagonColor == botRightHex.GetComponent<Hexagon>().hexagonColor && hexagonColor == botHex.GetComponent<Hexagon>().hexagonColor)
            {
                if (!matchedNeighbours.Contains(gameObject))
                {
                    matchedNeighbours.Add(gameObject);
                }
                if (!matchedNeighbours.Contains(botRightHex))
                {
                    matchedNeighbours.Add(botRightHex);
                }
                if (!matchedNeighbours.Contains(botHex))
                {
                    matchedNeighbours.Add(botHex);
                }
            }
        }
        if (botHex != null && botLeftHex != null)
        {
            if (hexagonColor == botHex.GetComponent<Hexagon>().hexagonColor && hexagonColor == botLeftHex.GetComponent<Hexagon>().hexagonColor)
            {
                if (!matchedNeighbours.Contains(gameObject))
                {
                    matchedNeighbours.Add(gameObject);
                }
                if (!matchedNeighbours.Contains(botHex))
                {
                    matchedNeighbours.Add(botHex);
                }
                if (!matchedNeighbours.Contains(botLeftHex))
                {
                    matchedNeighbours.Add(botLeftHex);
                }
            }
        }
        if (botLeftHex != null && upLeftHex != null)
        {
            if (hexagonColor == botLeftHex.GetComponent<Hexagon>().hexagonColor && hexagonColor == upLeftHex.GetComponent<Hexagon>().hexagonColor)
            {
                if (!matchedNeighbours.Contains(botLeftHex) && !matchedNeighbours.Contains(upLeftHex))
                {
                    if (!matchedNeighbours.Contains(gameObject))
                    {
                        matchedNeighbours.Add(gameObject);
                    }
                    if (!matchedNeighbours.Contains(botLeftHex))
                    {
                        matchedNeighbours.Add(botLeftHex);
                    }
                    if (!matchedNeighbours.Contains(upLeftHex))
                    {
                        matchedNeighbours.Add(upLeftHex);
                    }
                }
            }
        }

        //check axis
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && InputManager.getInput)
        {
            GameManager.instance.selectedHex = this.gameObject;
            if (InputManager.getInput)
            {
                HexSelectHandler.instance.FindNeighbours(column, row);
                HexSelectHandler.selectIndex++;
                HexSelectHandler.instance.MakeGroupOfHexes();
            }
        }
    }

    void AddHexes()
    {
        try
        {
            for (int i = 0; i < neighbours.Count; i++)
            {

                switch (i)
                {
                    case 0:
                        upLeftHex = neighbours[0].gameObject;
                        break;
                    case 1:
                        upHex = neighbours[1].gameObject;
                        break;
                    case 2:
                        upRightHex = neighbours[2].gameObject;
                        break;
                    case 3:
                        botRightHex = neighbours[3].gameObject;
                        break;
                    case 4:
                        botHex = neighbours[4].gameObject;
                        break;
                    case 5:
                        botLeftHex = neighbours[5].gameObject;
                        break;
                    default:
                        Debug.Log("Hex not found");
                        break;

                }
            }
        }

        catch
        {
            Debug.Log("Hex not found");
        }

    }
    void ClearHexes()
    {
        upLeftHex = null;
        upHex = null;
        upRightHex = null;
        botRightHex = null;
        botHex = null;
        botLeftHex = null;
    }
}
