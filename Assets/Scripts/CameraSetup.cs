using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private GridManager gridManager;
    public int borderSize;

    private void Awake()
    {
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    private void Start()
    {
        SetCameraFov();
    }
    void SetCameraFov()
    {
        int tileWidth = gridManager.columnsSize;
        int tileHeight = gridManager.rowsSize;

        transform.position = new Vector3((float)(tileWidth -1) / 2f, (float)(tileHeight -1) / 2, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;

        float verticalSize = (float)tileHeight / 2f + (float)borderSize;

        float horizontalSize = ((float)tileWidth / 2f + (float)borderSize) / aspectRatio;

        if (verticalSize > horizontalSize)
        {
            this.GetComponent<Camera>().orthographicSize = verticalSize;
        }
        else
        {
            this.GetComponent<Camera>().orthographicSize = horizontalSize;
        }
    }
}
