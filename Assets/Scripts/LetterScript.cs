using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterScript : MonoBehaviour
{
    public static bool hasSelected = false;
    public static Letter latestSelected = null;
    public static bool IsVertical = true;
    public bool IsSelected = false;
    public SpriteRenderer sprite;
    public SpriteRenderer background;
    public Color defaultBackgroundColor = Color.white;
    public Color selectedBackgroundColor = Color.magenta;
    public Color connectedBackgroundColor = Color.red;
    private Letter letter;
    [SerializeField] public Sprite letterA;
    [SerializeField] public Sprite letterB;
    [SerializeField] public Sprite letterC;
    [SerializeField] public Sprite letterD;
    [SerializeField] public Sprite letterE;
    [SerializeField] public Sprite letterF;
    [SerializeField] public Sprite letterG;
    [SerializeField] public Sprite letterH;
    [SerializeField] public Sprite letterI;
    [SerializeField] public Sprite letterJ;
    [SerializeField] public Sprite letterK;
    [SerializeField] public Sprite letterL;
    [SerializeField] public Sprite letterM;
    [SerializeField] public Sprite letterN;
    [SerializeField] public Sprite letterO;
    [SerializeField] public Sprite letterP;
    [SerializeField] public Sprite letterQ;
    [SerializeField] public Sprite letterR;
    [SerializeField] public Sprite letterS;
    [SerializeField] public Sprite letterT;
    [SerializeField] public Sprite letterU;
    [SerializeField] public Sprite letterV;
    [SerializeField] public Sprite letterW;
    [SerializeField] public Sprite letterX;
    [SerializeField] public Sprite letterY;
    [SerializeField] public Sprite letterZ;
    [SerializeField] public Sprite letter0;
    [SerializeField] public Sprite letter1;
    [SerializeField] public Sprite letter2;
    [SerializeField] public Sprite letter3;
    [SerializeField] public Sprite letter4;
    [SerializeField] public Sprite letter5;
    [SerializeField] public Sprite letter6;
    [SerializeField] public Sprite letter7;
    [SerializeField] public Sprite letter8;
    [SerializeField] public Sprite letter9;
    [SerializeField] public Sprite empty;

    private List<Letter> _connectedLetters = null;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        background = GetComponentsInChildren<SpriteRenderer>()[2];
        background.color = defaultBackgroundColor;
        var position = gameObject.transform.position;
        int posX = (int) position.x;
        int posY = (int) position.y * -1;
        letter = OnLoadInit.Crossword.tiles[posY, posX];
        letter.FieldObject = gameObject;
        sprite.sprite = empty;
    }

    public void UpdateColor()
    {
        background.color = letter.Color;
    }

    private void FixedUpdate()
    {
        if (!IsSelected) UpdateColor();
    }

    private void OnMouseOver()
    {
        if (!hasSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                IsVertical = !IsVertical;
                foreach (var letter in _connectedLetters)
                {
                    var scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                    scriptComponent.background.color = letter.Color;
                }

                UpdateConnectedLetters();
            }

            if (Input.GetMouseButtonDown(2) && !hasSelected &&
                !GameController.players[GameController._playerIndex].MadeTurn &&
                GameController.players[GameController._playerIndex].Skips == 0)
            {
                /*Player player = GameController.players[GameController._playerIndex];
                
                if (!player.MadeTurn && !letter.IsRevealed)
                {
                    player.Score += 100;
                    player.MadeTurn = true;
                    RevealWord();
                }*/
                IsSelected = true;
                GameController.selectedLetter = this;
                foreach (var letter in _connectedLetters)
                {
                    var scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                    scriptComponent.background.color = selectedBackgroundColor;
                    scriptComponent.IsSelected = true;
                }

                hasSelected = true;
                latestSelected = letter;
                GameController.guessUIPanelS.SetActive(true);
            }

            if (Input.GetMouseButtonDown(3))
            {
                Conceal();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(2))
            {
                hasSelected = false;
                var script = latestSelected.FieldObject.GetComponent<LetterScript>();
                script.background.color = defaultBackgroundColor;
                foreach (var letterx in script._connectedLetters)
                {
                    var scriptComponent = letterx.FieldObject.GetComponent<LetterScript>();
                    scriptComponent.background.color = defaultBackgroundColor;
                    scriptComponent.IsSelected = false;
                }

                script._connectedLetters = null;
                script.IsSelected = false;
                IsSelected = false;
                UpdateVerticality();
                UpdateConnectedLetters();
                GameController.guessUIPanelS.SetActive(false);
            }
        }
    }

    private void UpdateConnectedLetters()
    {
        GetConnectedLetters();
        foreach (var letter in _connectedLetters)
        {
            var scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
            scriptComponent.background.color = connectedBackgroundColor;
        }

        background.color = selectedBackgroundColor;
    }

    private void UpdateVerticality()
    {
        var tiles = OnLoadInit.Crossword.tiles;
        var position = gameObject.transform.position;
        Vector2 initPos = new Vector2((int) position.x, (int) position.y * -1);
        int checks = 0;
        if (IsVertical)
        {
            if (initPos.y > 0)
            {
                if (tiles[(int) initPos.y - 1, (int) initPos.x].IsEmpty)
                {
                    checks++;
                }
            }

            if (initPos.y < Crossword.ySize)
            {
                if (tiles[(int) initPos.y + 1, (int) initPos.x].IsEmpty)
                {
                    checks++;
                }
            }

            if (checks == 2) IsVertical = !IsVertical;
        }
        else
        {
            if (initPos.x > 0)
            {
                if (tiles[(int) initPos.y, (int) initPos.x - 1].IsEmpty)
                {
                    checks++;
                }
            }

            if (initPos.x < Crossword.ySize)
            {
                if (tiles[(int) initPos.y, (int) initPos.x + 1].IsEmpty)
                {
                    checks++;
                }
            }

            if (checks == 2) IsVertical = !IsVertical;
        }
    }

    public void RevealLetter()
    {
        sprite.sprite = LoadSpriteByChar(letter.Character);
        letter.IsRevealed = true;
    }

    public void RevealWord()
    {
        foreach (var letter in _connectedLetters)
        {
            if (!letter.IsRevealed)
            {
                LetterScript scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                SpriteRenderer[] spriteRenderers = letter.FieldObject.GetComponentsInChildren<SpriteRenderer>();
                SpriteRenderer letterSprite = null;
                foreach (var renderer in spriteRenderers)
                {
                    if (renderer.CompareTag("LetterSprite")) letterSprite = renderer;
                }

                letterSprite.sprite = LoadSpriteByChar(letter.Character);
                letter.IsRevealed = true;
            }
        }
    }

    public void Conceal()
    {
        
    }

    private bool CompareWords(string inputText)
    {
        if (inputText.Length != _connectedLetters.Count) return false;
        for (int i = 0; i < inputText.Length; i++)
        {
            if (inputText[i] != _connectedLetters[i].Character) return false;
        }

        return true;
    }

    private void RecursionOpening(ref int score, LetterScript letterScript, char c, ref List<Vector2> discovered,
        bool initVert)
    {
        IsVertical = !initVert;
        letterScript.GetConnectedLetters();
        foreach (var letter in letterScript._connectedLetters)
        {
            Debug.Log("Letter " + letter.Character + " is being checked    " + letter.IsRevealed + "   " +
                      letter.Character);
            if (!letter.IsRevealed && letter.Character == c)
            {
                LetterScript scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                Debug.Log("Letter " + scriptComponent.letter.Character + " is at cross: " +
                          scriptComponent.IsLetterAtCross() + ". Discovered list size: " + discovered.Count +
                          " Connected letters list: " + letterScript._connectedLetters.Count);
                bool contains = false;
                foreach (var VARIABLE in discovered)
                {
                    if (scriptComponent.letter.X != (int) VARIABLE.x && scriptComponent.letter.Y != (int) VARIABLE.y)
                        contains = true;
                    //Debug.Log(scriptComponent.letter.X + " " + (int) VARIABLE.x + " " + scriptComponent.letter.Y + " " + (int) VARIABLE.y);
                }

                discovered.Add(new Vector2(scriptComponent.letter.X, scriptComponent.letter.X));
                score += 100;
                SpriteRenderer[] spriteRenderers =
                    scriptComponent.letter.FieldObject.GetComponentsInChildren<SpriteRenderer>();
                SpriteRenderer letterSprite = null;
                foreach (var renderer in spriteRenderers)
                {
                    if (renderer.CompareTag("LetterSprite")) letterSprite = renderer;
                }

                scriptComponent.sprite.sprite = LoadSpriteByChar(c);
                scriptComponent.letter.IsRevealed = true;
                scriptComponent.letter.Color = GameController.players[GameController._playerIndex].Color;
                scriptComponent.background.color = scriptComponent.letter.Color;
                if (!scriptComponent.IsLetterAtCross() && !contains)
                {
                    Debug.Log("Added letter x: " + scriptComponent.letter.X + " y: " + scriptComponent.letter.X +
                              " letter char: " + scriptComponent.letter.Character);
                    if (discovered.Count <= 100)
                        RecursionOpening(ref score, scriptComponent, c, ref discovered, !initVert);
                }
            }
        }
    }

    public void TryToGuess(char c)
    {
        int score = 0;
        Player player = GameController.players[GameController._playerIndex];
        var inputField = GameController.guessUIPanelS.GetComponentInChildren<InputField>();
        if (inputField.text.Length > 0)
        {
            if (CompareWords(inputField.text))
            {
                foreach (var letter in _connectedLetters)
                {
                    if (!letter.IsRevealed)
                    {
                        score += 100;
                        LetterScript scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                        SpriteRenderer[] spriteRenderers = letter.FieldObject.GetComponentsInChildren<SpriteRenderer>();
                        SpriteRenderer letterSprite = null;
                        foreach (var renderer in spriteRenderers)
                        {
                            if (renderer.CompareTag("LetterSprite")) letterSprite = renderer;
                        }

                        letterSprite.sprite = LoadSpriteByChar(letter.Character);
                        letter.IsRevealed = true;
                        letter.Color = GameController.players[GameController._playerIndex].Color;
                        scriptComponent.background.color = scriptComponent.letter.Color;
                        if (scriptComponent.IsLetterAtCross())
                        {
                            List<Vector2> discovered = new List<Vector2>();
                            discovered.Add(new Vector2(letter.X, letter.Y));
                            RecursionOpening(ref score, scriptComponent, letter.Character, ref discovered, IsVertical);
                        }
                    }
                }

                player.MadeTurn = false;
            }
            else
            {
                string word = "";
                foreach (var VARIABLE in _connectedLetters)
                {
                    word += VARIABLE.Character;
                }

                player.MadeTurn = true;
                player.Skips = 2;
            }
        }
        else
        {
            foreach (var letter in _connectedLetters)
            {
                if (!letter.IsRevealed && letter.Character == c)
                {
                    score += 100;
                    LetterScript scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                    SpriteRenderer[] spriteRenderers = letter.FieldObject.GetComponentsInChildren<SpriteRenderer>();
                    SpriteRenderer letterSprite = null;
                    foreach (var renderer in spriteRenderers)
                    {
                        if (renderer.CompareTag("LetterSprite")) letterSprite = renderer;
                    }

                    letterSprite.sprite = LoadSpriteByChar(letter.Character);
                    letter.IsRevealed = true;
                    letter.Color = GameController.players[GameController._playerIndex].Color;
                    scriptComponent.background.color = scriptComponent.letter.Color;
                    if (scriptComponent.IsLetterAtCross())
                    {
                        List<Vector2> discovered = new List<Vector2>();
                        discovered.Add(new Vector2(letter.X, letter.Y));
                        RecursionOpening(ref score, scriptComponent, letter.Character, ref discovered, IsVertical);
                    }
                }
            }

            player.MadeTurn = true;
        }

        player.Score += score;
        hasSelected = false;
        var script = latestSelected.FieldObject.GetComponent<LetterScript>();
        script.background.color = defaultBackgroundColor;
        foreach (var letterx in script._connectedLetters)
        {
            var scriptComponentx = letterx.FieldObject.GetComponent<LetterScript>();
            scriptComponentx.IsSelected = false;
            scriptComponentx.background.color = defaultBackgroundColor;
        }

        script._connectedLetters = null;
        inputField.text = "";
        GameController.guessUIPanelS.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (!hasSelected)
        {
            IsSelected = true;
            UpdateVerticality();
            UpdateConnectedLetters();
            foreach (var VARIABLE in _connectedLetters)
            {
                VARIABLE.FieldObject.GetComponent<LetterScript>().IsSelected = true;
            }
        }
    }

    public bool IsLetterAtCross()
    {
        var verticality = IsVertical;
        GetConnectedLetters();
        if (!(_connectedLetters.Count > 1)) return false;
        IsVertical = !IsVertical;
        GetConnectedLetters();
        if (!(_connectedLetters.Count > 1))
        {
            IsVertical = verticality;
            return false;
        }

        IsVertical = verticality;
        return true;
    }

    public void GetConnectedLetters()
    {
        List<Letter> lettersList = new List<Letter>();
        var tiles = OnLoadInit.Crossword.tiles;
        var position = gameObject.transform.position;
        Vector2 initPos = new Vector2((int) position.x, (int) position.y * -1);
        if (IsVertical)
        {
            Vector2 curPos = initPos;
            while (true)
            {
                if (curPos.y >= 0)
                {
                    if (!tiles[(int) curPos.y, (int) initPos.x].IsEmpty)
                    {
                        lettersList.Add(tiles[(int) curPos.y, (int) initPos.x]);
                        curPos.y -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            curPos = initPos;
            curPos.y += 1;
            while (true)
            {
                if (curPos.y < Crossword.ySize)
                {
                    if (!tiles[(int) curPos.y, (int) initPos.x].IsEmpty)
                    {
                        lettersList.Add(tiles[(int) curPos.y, (int) initPos.x]);
                        curPos.y += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            lettersList.Sort((l1, l2) => l1.Y.CompareTo(l2.Y));
            _connectedLetters = lettersList;
        }
        else
        {
            Vector2 curPos = initPos;
            while (true)
            {
                if (curPos.x >= 0)
                {
                    if (!tiles[(int) initPos.y, (int) curPos.x].IsEmpty)
                    {
                        lettersList.Add(tiles[(int) initPos.y, (int) curPos.x]);
                        curPos.x -= 1;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            curPos = initPos;
            curPos.x += 1;
            while (true)
            {
                if (curPos.x < Crossword.xSize)
                {
                    if (!tiles[(int) initPos.y, (int) curPos.x].IsEmpty)
                    {
                        lettersList.Add(tiles[(int) initPos.y, (int) curPos.x]);
                        curPos.x += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            lettersList.Sort((l1, l2) => l1.X.CompareTo(l2.X));
            _connectedLetters = lettersList;
        }
    }


    private void OnMouseExit()
    {
        if (!hasSelected)
        {
            foreach (var letter in _connectedLetters)
            {
                var scriptComponent = letter.FieldObject.GetComponent<LetterScript>();
                scriptComponent.IsSelected = false;
                scriptComponent.background.color = letter.Color;
            }

            _connectedLetters = null;
            IsSelected = false;
        }
    }

    Sprite LoadSpriteByChar(char ch)
    {
        switch (ch)
        {
            case 'a': return letterA;
            case 'b': return letterB;
            case 'c': return letterC;
            case 'd': return letterD;
            case 'e': return letterE;
            case 'f': return letterF;
            case 'g': return letterG;
            case 'h': return letterH;
            case 'i': return letterI;
            case 'j': return letterJ;
            case 'k': return letterK;
            case 'l': return letterL;
            case 'm': return letterM;
            case 'n': return letterN;
            case 'o': return letterO;
            case 'p': return letterP;
            case 'q': return letterQ;
            case 'r': return letterR;
            case 's': return letterS;
            case 't': return letterT;
            case 'u': return letterU;
            case 'v': return letterV;
            case 'w': return letterW;
            case 'x': return letterX;
            case 'y': return letterY;
            case 'z': return letterZ;
            case '0': return letter0;
            case '1': return letter1;
            case '2': return letter2;
            case '3': return letter3;
            case '4': return letter4;
            case '5': return letter5;
            case '6': return letter6;
            case '7': return letter7;
            case '8': return letter8;
            case '9': return letter9;
            default: return empty;
        }
    }
}