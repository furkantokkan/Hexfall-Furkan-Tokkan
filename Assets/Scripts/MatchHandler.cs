using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchHandler : MonoBehaviour
{
    SwitchHexHandler switchHexHandler;

    public List<GameObject> allMatchesList = new List<GameObject>();

    internal List<GameObject> firstHexMatch = new List<GameObject>();
    internal List<GameObject> secondHexMatch = new List<GameObject>();
    internal List<GameObject> thirdHexMatch = new List<GameObject>();

    private void Awake()
    {
        switchHexHandler = GetComponent<SwitchHexHandler>();
    }

    private void Update()
    {
        if (allMatchesList.Count >= 3)
        {
            for (int i = 0; i < allMatchesList.Count; i++)
            {
                allMatchesList[i].GetComponent<SpriteRenderer>().color = Color.red;
                if (i == allMatchesList.Count - 1)
                {
                    allMatchesList.Clear();
                }
            }
        }
    }

    public void CheckMatch(Hexagon hex)
    {
        HexSelectHandler.instance.FindNeighbours(hex);
        if (HexSelectHandler.instance.neighboursList != null)
        {
            for (int i = 0; i < HexSelectHandler.instance.neighboursList.Count; i++)
            {
                if (HexSelectHandler.instance.neighboursList[i] != null)
                {
                    if (HexSelectHandler.instance.neighboursList[i].GetComponent<Hexagon>().hexagonColor == hex.hexagonColor)
                    {
                        allMatchesList.Add(HexSelectHandler.instance.neighboursList[i].gameObject);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }

}
