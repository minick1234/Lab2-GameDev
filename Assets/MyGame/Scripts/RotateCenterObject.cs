using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCenterObject : MonoBehaviour
{
    public float MouseMovementSensitivty;
    public float MouseX;
    public float MouseY;

    public bool AllowClickOn = true;
    public bool ClickedOnObject = false;

    public GameObject ObjectToRotate;

    public float XRotationDegrees = 0;
    public float YRotationDegrees = 0;

    public bool RotateAutomatically = false;

    public float AutomaticXRotation = 2f;
    public float AutomaticYRotation = 2f;

    public bool AllowYRotation = false;

    // Update is called once per frame
    void Update()
    {
        if (!ClickedOnObject)
        {
            Cursor.lockState = CursorLockMode.None;
            RotateAutomatically = true;
            Cursor.visible = true;
        }

        if (AllowClickOn)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0) && ClickedOnObject)
            {
                ClickedOnObject = false;
                RotateAutomatically = true;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (EventSystem.current.IsPointerOverGameObject() && !ClickedOnObject)
                {
                    ClickedOnObject = false;
                    RotateAutomatically = true;
                }
                else
                {
                    RaycastHit HitInfo;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out HitInfo, Mathf.Infinity,
                            ~(1 << 5)))
                    {
                        MouseX = Input.GetAxis("Mouse X");
                        MouseY = Input.GetAxis("Mouse Y");
                        ClickedOnObject = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        RotateAutomatically = false;
                        Cursor.visible = false;
                    }
                }
            }
        }


        if (ClickedOnObject && !RotateAutomatically)
        {
            XRotationDegrees = MouseX * MouseMovementSensitivty * Time.deltaTime * Mathf.Rad2Deg;

            if (AllowYRotation)
            {
                YRotationDegrees = MouseY * MouseMovementSensitivty * Time.deltaTime * Mathf.Rad2Deg;
                ObjectToRotate.transform.RotateAround(Vector3.forward, -YRotationDegrees);
            }

            ObjectToRotate.transform.RotateAround(Vector3.up, -XRotationDegrees);
        }
        else if (RotateAutomatically && !ClickedOnObject)
        {
            ObjectToRotate.transform.RotateAround(Vector3.up, AutomaticXRotation * Time.deltaTime);
            if (AllowYRotation)
            {
                ObjectToRotate.transform.RotateAround(Vector3.forward, AutomaticYRotation * Time.deltaTime);
            }
        }
    }
}