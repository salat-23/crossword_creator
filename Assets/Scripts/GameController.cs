using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button nextTurnButton;
    [SerializeField] private GameObject guessUIPanel;
    public static GameObject guessUIPanelS;
    
    public static List<Player> players;
    [SerializeField] public List<GameObject> panels;
    [SerializeField] public List<Material> colors;
    public static int _playerIndex = 0;
    public static LetterScript selectedLetter;
    public char latestInputCharacter = 'a';
    public void UpdateLeaderboards()
    {
        for (int i = 0; i < 8; i++)
        {
            Player player = players[i];
            GameObject panel = panels[i];
            var texts = panel.GetComponentsInChildren<Text>();
            Text name = null;
            Text score = null;
            Text skip = null;
            Image image = panel.GetComponent<Image>();
            if (i == _playerIndex) image.color = Color.grey;
            else
            {
                image.color = colors[i].color;
            }
            foreach (var VARIABLE in texts)
            {
                if (VARIABLE.CompareTag("NameText")) name = VARIABLE;
                if (VARIABLE.CompareTag("Score")) score = VARIABLE;
                if (VARIABLE.CompareTag("Skip")) skip = VARIABLE;
            }
            name.text = player.Name;
            score.text = player.Score.ToString();
            if (player.Skips > 0) skip.text = "Skips";
            else skip.text = "Plays";
        }
        Player currentPlayer = players[_playerIndex];
        if (currentPlayer.MadeTurn)
        {
            nextTurnButton.image.color = Color.green;
        }
        else
        {
            nextTurnButton.image.color = Color.red;
        }

        var buttons = guessUIPanel.GetComponentsInChildren<Text>();
        Text text = null;
        foreach (var VARIABLE in buttons)
        {
            if (VARIABLE.CompareTag("Confirm"))
            {
                text = VARIABLE;
            }
        }
        text.text = "Confirm: " + latestInputCharacter;
    }
    private void FixedUpdate()
    {
        UpdateLeaderboards();
    }
    private void Start()
    {
        guessUIPanelS = guessUIPanel;
        players = new List<Player>();
        nextTurnButton.onClick.AddListener(OnNextTurnClick);
        for (int i = 0; i < 8; i++)
        {
            Player player = new Player("player" + (i + 1), colors[i].color);
            players.Add(player);
        }
    }
    private void OnNextTurnClick()
    {
        foreach (var player in players)
        {
            player.MadeTurn = false;
        }
        Player playerx = players[_playerIndex];
        if (playerx.Skips > 0) playerx.Skips -= 1;
        if (_playerIndex <= 6) _playerIndex++;
        else _playerIndex = 0;
    }

    public void ChooseChar(string c)
    { 
        latestInputCharacter = c[0];
    }

    public void ConfirmChar()
    {
        selectedLetter.TryToGuess(latestInputCharacter);
    }
}