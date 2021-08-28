using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NameChangeScript : MonoBehaviour
{
    private int index = 0;

    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private InputField inputField;
    [SerializeField] private GameController _controller;


    private void Start()
    {
        inputField.text = "";
        okButton.onClick.AddListener(OnOkClick);
        cancelButton.onClick.AddListener(OnCancelClick);
    }

    public void SetPlayer(int indexF)
    {
        this.index = indexF;
        gameObject.SetActive(true);
        inputField.text = GameController.players[indexF].Name;
        Debug.Log("indexF: " + indexF);
        Debug.Log("index: " + index);
        Debug.Log("players count: " + GameController.players.Count);
    }

    private void OnOkClick()
    {
        GameController.players[index].Name = inputField.text;
        gameObject.SetActive(false);
        LeaderBoardPlayerScript.IsAnotherWindowActive = false;
    }

    private void OnCancelClick()
    {
        gameObject.SetActive(false);
        LeaderBoardPlayerScript.IsAnotherWindowActive = false;
    }
}