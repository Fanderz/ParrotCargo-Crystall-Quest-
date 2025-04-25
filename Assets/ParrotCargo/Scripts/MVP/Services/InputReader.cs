using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private float _width;
    private float _height;

    public bool IsTouched { get; private set; }
    public Vector3 TouchPosition { get; private set; }

    private void Awake()
    {
        _width = (float)Screen.width / 2.0f;
        _height = (float)Screen.height / 2.0f;

        TouchPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            IsTouched = true;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchPosition = touch.position;

                if (touch.phase == TouchPhase.Moved)
                    TouchPosition = new Vector3(-((TouchPosition.x - _width) / _width), (TouchPosition.y - _height) / _height, 0.0f);
            }
            else
            {
                TouchPosition = Input.mousePosition;
            }
        }

        if(IsTouched && Input.touchCount == 0 || Input.GetMouseButtonUp(0) == false)
        {
            IsTouched = false;
        }
    }

    //private bool GetBoolAsTrigger(ref bool value)
    //{
    //    bool localValue = value;
    //    value = false;

    //    return localValue;
    //}
}
