using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadInit : MonoBehaviour
{
    public static Crossword Crossword = new Crossword();
    [SerializeField] private GameObject letterObject;

    private void Start()
    {
        Crossword.ImportString(StringStorage.FINAL);
        for (int y = 0; y < Crossword.ySize; y++)
        {
            for (int x = 0; x < Crossword.xSize; x++)
            {
                if (!Crossword.tiles[y, x].IsEmpty) Instantiate(letterObject, new Vector3(x, y*-1, 0), Quaternion.identity);
            }
        }
    }
    
    
}
