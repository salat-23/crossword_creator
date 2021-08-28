using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardPlayerScript : MonoBehaviour
{
    public static bool IsAnotherWindowActive = false;
    [SerializeField] private NameChangeScript _nameChangeScript;
    [SerializeField] private int _playerInteregInList = 0;
    [SerializeField] private Button _button;
    
    public delegate void LeaderboardsUpdated();
    public static event LeaderboardsUpdated OnLeaderboardsUpdated;
    private void Start()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        IsAnotherWindowActive = true;
       _nameChangeScript.SetPlayer(_playerInteregInList);
    }
}
