using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter
{
    public char Character;
    public GameObject FieldObject;
    public int X, Y;
    public bool IsEmpty;
    public bool IsRevealed;
    public Color Color;


    public Letter(int x, int y)
    {
        X = x;
        Y = y;
        IsEmpty = true;
        IsRevealed = false;
        Character = '#';
        Color = Color.white;
    }

    public void SetCharacter(char c)
    {
        Character = c;
    }
}