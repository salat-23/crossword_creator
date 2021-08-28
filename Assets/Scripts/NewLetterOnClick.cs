using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class NewLetterOnClick : MonoBehaviour
{
    [SerializeField] private GameObject letterBox;
    Camera mainCamera;
    

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        Debug.Log("Click started");
        Vector3 pos = transform.position;
        Debug.Log(pos.x + " " + pos.y);
        Instantiate(letterBox, pos, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
        Debug.Log("Click ended");
    }
}
