using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject selectedHex;

    public Color[] colors;

    public List<GameObject> selectedHexesList = new List<GameObject>();


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
    }
    private void Update()
    {
        if (selectedHexesList != null && selectedHexesList.Count > 0)
        {
            selectedHexesList[0].GetComponent<Hexagon>().onSelected?.Invoke();
            selectedHexesList[1].GetComponent<Hexagon>().onSelected?.Invoke();
            selectedHexesList[2].GetComponent<Hexagon>().onSelected?.Invoke();

        }
    }

}
