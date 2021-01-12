using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchHandler : MonoBehaviour
{
    SwitchHexHandler switchHexHandler;

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
            if (firstHexMatch.Count >= 3)
            {
                stopRoutine = true;
                for (int i = 0; i < firstHexMatch.Count; i++)
                {
                    GridManager.hexArray[firstHexMatch[i].GetComponent<Hexagon>().column,
                        firstHexMatch[i].GetComponent<Hexagon>().row] = null;

                    Destroy(firstHexMatch[i].gameObject);
                }
            }
            if (secondHexMatch.Count >= 3)
            {
                stopRoutine = true;
                for (int i = 0; i < secondHexMatch.Count; i++)
                {
                    GridManager.hexArray[secondHexMatch[i].GetComponent<Hexagon>().column,
                              secondHexMatch[i].GetComponent<Hexagon>().row] = null;

                    Destroy(secondHexMatch[i].gameObject);
                }
            }
            if (thirdHexMatch.Count >= 3)
            {
                stopRoutine = true;
                for (int i = 0; i < thirdHexMatch.Count; i++)
                {
                    GridManager.hexArray[thirdHexMatch[i].GetComponent<Hexagon>().column,
                              thirdHexMatch[i].GetComponent<Hexagon>().row] = null;

                    Destroy(thirdHexMatch[i].gameObject);
                }
            }
        }
        catch
        {


        }

    }

    public void CheckMatch()
    {
        firstHexMatch.AddRange(switchHexHandler.firstHex.GetComponent<Hexagon>().matchedNeighbours);
        secondHexMatch.AddRange(switchHexHandler.secondHex.GetComponent<Hexagon>().matchedNeighbours);
        thirdHexMatch.AddRange(switchHexHandler.thirdHex.GetComponent<Hexagon>().matchedNeighbours);
    }

}
