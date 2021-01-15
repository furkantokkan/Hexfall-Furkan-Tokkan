﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float sensivity = 5f;

    private Vector2 lastClickPos;
    private Vector3 direction;

    public static bool getInput = true;

    private bool hexSelected;

    private SwitchHexHandler switchHexHandler;


    private void Awake()
    {
        switchHexHandler = FindObjectOfType<SwitchHexHandler>();
    }

    void Update()
    {
        if (Application.isMobilePlatform && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (getInput && !GameManager.instance.gameOver)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);

                        if (Physics.Raycast(ray, out hit))
                        {
                            GameManager.instance.selectedHex = hit.collider.gameObject;

                        }

                        lastClickPos = touch.position;
                        HexSelectHandler.selectIndex++;
                        break;
                    case TouchPhase.Moved:
                        direction = lastClickPos - touch.position;
                        lastClickPos = touch.position;
                        direction = new Vector3(direction.x, 0, 0);
                        if (direction.x > sensivity)
                        {

                            if (GameManager.instance.selectedHex != null)
                            {
                                //left
                                switchHexHandler.MoveRight();
                            }

                        }
                        else if (direction.x < -sensivity)
                        {
                            if (GameManager.instance.selectedHex != null)
                            {
                                //right
                                switchHexHandler.MoveRight();
                            }
                        }
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        if (direction.x < -sensivity && direction.x > sensivity)
                        {
                            hexSelected = false;
                        }
                        else
                        {
                            hexSelected = true;
                        }
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            if (getInput && !GameManager.instance.gameOver && GameManager.instance.selectedHex != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    lastClickPos = Input.mousePosition;
                }

                if (Input.GetMouseButton(1))
                {
                    Vector3 direction = new Vector3(lastClickPos.x, lastClickPos.y, 0) - Input.mousePosition;
                    lastClickPos = Input.mousePosition;
                    direction = new Vector3(direction.x, 0, 0);
                    if (direction.x > 0)
                    {
                        //left
                        switchHexHandler.MoveRight();

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
