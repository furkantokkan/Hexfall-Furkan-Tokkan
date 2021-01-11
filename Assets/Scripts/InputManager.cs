using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 lastClickPos;

    public static bool getInput = true;

    private SwitchHexHandler switchHexHandler;

    private void Awake()
    {
        switchHexHandler = FindObjectOfType<SwitchHexHandler>();
    }

    void Update()
    {
        if (Application.isMobilePlatform)
        {

        }
        else
        {
            if (getInput && GameManager.instance.selectedHexesList != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    lastClickPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(1))
                {
                    Vector3 direction = lastClickPos - Input.mousePosition;
                    lastClickPos = Input.mousePosition;
                    direction = new Vector3(direction.x, 0, 0);
                    if (direction.x > 0)
                    {
                        //left

                    }
                    else if (direction.x < 0)
                    {
                        //right
                        switchHexHandler.MoveRight();
                    }
                }
            }
        }
    }
}
