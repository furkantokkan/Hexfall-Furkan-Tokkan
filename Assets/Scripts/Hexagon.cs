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
    internal bool isNearHex = false;

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

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        neighbours.AddRange(HexSelectHandler.instance.FindNeighbours(this));
        CheckMatchedNeighbours();
        AddHexes();
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
            try
            {
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

        if (GameManager.instance.selectedHexesList.Count > 0 && GameManager.instance.selectedHexesList != null)
        {
            GameManager.instance.selectedHexesList[0].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[1].GetComponent<Hexagon>().onDeselected?.Invoke();
            GameManager.instance.selectedHexesList[2].GetComponent<Hexagon>().onDeselected?.Invoke();
        }

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
        neighbours.Clear();
        neighbours.AddRange(HexSelectHandler.instance.FindNeighbours(this));
        InputManager.getInput = true;
    }

    void CheckMatchedNeighbours()
    {
        if (!matchedNeighbours.Contains(gameObject))
        {
            matchedNeighbours.Add(gameObject);
        }
        /*
                if (hexagonColor == upLeftHex.GetComponent<Hexagon>().hexagonColor || hexagonColor == upHex.GetComponent<Hexagon>().hexagonColor)
                {
                    if (!matchedNeighbours.Contains(upLeftHex) || !matchedNeighbours.Contains(upHex))
                    {
                        matchedNeighbours.Add(upLeftHex);
                        matchedNeighbours.Add(upHex);
                    }
                }
        */
        //check axis
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && InputManager.getInput)
        {
            GameManager.instance.selectedHex = this.gameObject;
            if (InputManager.getInput)
            {
                HexSelectHandler.instance.FindNeighbours(this);
                HexSelectHandler.selectIndex++;
                HexSelectHandler.instance.MakeGroupOfHexes();
            }
        }
    }

    void AddHexes()
    {
        for (int i = 0; i < neighbours.Count; i++)
        {

            try
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
            catch 
            {
                Debug.Log("Hex not found");
            }
 
        }
    }
}
