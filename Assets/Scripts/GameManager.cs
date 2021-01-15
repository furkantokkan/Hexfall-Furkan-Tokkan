using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private UIManager uıManager;


    [Header("Game Settings")]
    public Color[] colors;

    public int bombSpawnScore = 1000;

    [Range(3, 10)]
    public int minimumBombExplodeCount = 5, maximumBombExplodeCount = 7;

    [Header("Hexagon Settings")]
    public float hexSwitchSpeed = 1f;

    public int hexScoreAmount = 5;

    internal GameObject selectedHex;

    internal List<GameObject> selectedHexesList = new List<GameObject>();

    internal List<GameObject> allHexesList = new List<GameObject>();

    internal int score;

    internal int moves;

    internal int maxHexagonCount;

    internal bool canHexTakeNewPlace = false;

    internal Color bombColor;

    internal bool gameOver = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        uıManager = GameObject.Find("UI").GetComponent<UIManager>();
    }
    private void Start()
    {
        maxHexagonCount = allHexesList.Count;
    }
    private void OnEnable()
    {
        Hexagon.score += AddScore;
    }

    private void OnDisable()
    {
        Hexagon.score -= AddScore;
    }
    private void Update()
    {
        if (selectedHexesList != null && selectedHexesList.Count > 0)
        {
            selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
            selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
            selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();
        }
       
        if (bombColor.a > 0)
        {
            for (int i = 0; i < allHexesList.Count; i++)
            {
                if (allHexesList[i].GetComponent<Hexagon>().hexagonColor == bombColor)
                {
                    Destroy(allHexesList[i].gameObject);
                }
                if (i == allHexesList.Count)
                {
                    bombColor.a = 0;
                }
            }
        }
        
    }

    void AddScore(int amount)
    {
        score += amount;
        uıManager.UpdateScoreText();
    }

}
