using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class Crossword
{
    public const int xSize = 100;
    public const int ySize = 100;

    public Letter[,] tiles;

    public Crossword()
    {
        tiles = new Letter[xSize, ySize];
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                tiles[y, x] = new Letter(x, y);
            }
        }
    }

    public void ImportString(string str)
    {
        int x = 0;
        int y = 0;
        foreach (var ch in str)
        {
            if (ch.Equals('\n'))
            {
                y++;
                x = 0;
            }
            else if (new Regex("[A-Za-z0-9]").IsMatch(ch.ToString()))
            {
                tiles[y, x].SetCharacter(ch);
                x++;
            }
            else
            {
                x++;
            }
        }
        for (int ty = 0; ty < ySize; ty++)
        {
            for (int tx = 0; tx < xSize; tx++)
            {
                if (!tiles[ty, tx].Character.Equals('#'))
                {
                    tiles[ty, tx].IsEmpty = false;
                }
                
            }
        }
    }

    public string GetString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                stringBuilder.Append(tiles[y, x].Character);
            }
            stringBuilder.Append('\n');
        }

        return stringBuilder.ToString();
    }


}
