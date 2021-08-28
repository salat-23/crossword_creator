using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string Name;
    public int Score;
    public int Skips;
    public bool MadeTurn;
    public Color Color;

    public Player(string name, Color c)
    {
        Name = name;
        Score = 0;
        Skips = 0;
        MadeTurn = false;
        Color = c;
    }

}
