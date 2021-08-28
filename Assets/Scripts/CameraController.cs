using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed;
    private Vector3 dragOrigin;
    public float cameraSizeMax = 20f;
    public float cameraSizeMin = 3f;
    public float cameraSize = 5f;
    public float scrollSpeed;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        
        cameraSize += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * -1;
        cameraSize = Mathf.Clamp(cameraSize, cameraSizeMin, cameraSizeMax);
        _camera.orthographicSize = cameraSize;


        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
    }
}