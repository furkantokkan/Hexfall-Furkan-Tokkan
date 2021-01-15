using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchHandler : MonoBehaviour
{
    private SwitchHexHandler switchHexHandler;

    public List<GameObject> firstHexMatch = new List<GameObject>();
    public List<GameObject> secondHexMatch = new List<GameObject>();
    public List<GameObject> thirdHexMatch = new List<GameObject>();

    internal bool stopRoutine;


    private void Awake()
    {
        switchHexHandler = GetComponent<SwitchHexHandler>();
    }

    private void Update()
    {
        try
        {
            if (firstHexMatch.Count >= 3 && firstHexMatch != null)
            {
                stopRoutine = true;
                for (int i = 0; i < 3; i++)
                {
                    GridManager.hexArray[firstHexMatch[i].GetComponent<Hexagon>().column,
                        firstHexMatch[i].GetComponent<Hexagon>().row] = null;
                    Destroy(firstHexMatch[i].gameObject);
                }
                ClearMatch();
            }
            if (secondHexMatch.Count >= 3 && secondHexMatch != null)
            {
                stopRoutine = true;
                for (int i = 0; i < 3; i++)
                {
                    GridManager.hexArray[secondHexMatch[i].GetComponent<Hexagon>().column,
                              secondHexMatch[i].GetComponent<Hexagon>().row] = null;

                    Destroy(secondHexMatch[i].gameObject);
                }
                ClearMatch();
            }
            if (thirdHexMatch.Count >= 3 && thirdHexMatch != null)
            {
                stopRoutine = true;
                for (int i = 0; i < 3; i++)
                {
                    GridManager.hexArray[thirdHexMatch[i].GetComponent<Hexagon>().column,
                              thirdHexMatch[i].GetComponent<Hexagon>().row] = null;

                    Destroy(thirdHexMatch[i].gameObject);
                }
                ClearMatch();
            }
        }
        catch
        {
            Debug.Log("Hex Destroyed");
        }

    }

    public void AddMatch()
    {
        if (switchHexHandler.firstHex != null)
        {
            firstHexMatch.AddRange(switchHexHandler.firstHex.GetComponent<Hexagon>().matchedNeighbours);
        }
        if (switchHexHandler.secondHex != null)
        {
            secondHexMatch.AddRange(switchHexHandler.secondHex.GetComponent<Hexagon>().matchedNeighbours);
        }
        if (switchHexHandler.thirdHex != null)
        {
            thirdHexMatch.AddRange(switchHexHandler.thirdHex.GetComponent<Hexagon>().matchedNeighbours);
        }
    }
    public void ClearMatch()
    {
        firstHexMatch.Clear();
        secondHexMatch.Clear();
        thirdHexMatch.Clear();
    }

}
