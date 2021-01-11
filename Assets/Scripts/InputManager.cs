using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 lastClickPos;

    public static bool getInput = true;

    private SwitchHexHandler switchHexHandler;

    private void Awake()
    {
        switchHexHandler = FindObjectOfType<SwitchHexHandler>();
    }

    void Update()
    {
        if (Application.isMobilePlatform && Input.touchCount > 0 && getInput)
        {
            Touch touch = Input.GetTouch(0);

            if (getInput && GameManager.instance.selectedHexesList != null)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray,out hit))
                {
                    GameManager.instance.selectedHex = hit.collider.gameObject;
                }

                if (GameManager.instance.selectedHex != null)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            lastClickPos = touch.position;
                            HexSelectHandler.selectIndex++;
                            break;
                        case TouchPhase.Moved:
                            Vector3 direction = lastClickPos - touch.position;
                            lastClickPos = touch.position;
                            direction = new Vector3(direction.x, 0, 0);
                            if (direction.x > 3)
                            {
                                //left
                                switchHexHandler.MoveRight();

                            }
                            else if (direction.x < -3)
                            {
                                //right
                                switchHexHandler.MoveRight();
                            }
                            break;
                        case TouchPhase.Stationary:
                            break;
                        case TouchPhase.Ended:
                            break;
                        case TouchPhase.Canceled:
                            break;
                        default:
                            break;
                    }
                }

                if (Input.GetMouseButton(1))
                {
                    
                }
            }
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
                    Vector3 direction = new Vector3(lastClickPos.x, lastClickPos.y,0) - Input.mousePosition;
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
