using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;
    public Transform figures;

    //Position and teams
    private GameObject[,] position = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];




    private string currentPlayer = "white";

    private bool gameOver = false;
    void Start()
    {
        playerWhite = new GameObject[]
        {
            Create("whiteRock", 0, 0), Create("whiteKnight", 1, 0), Create("whiteBishop", 2, 0), Create("whiteQueen", 3, 0),
            Create("whiteKing", 4, 0), Create("whiteBishop", 5, 0), Create("whiteKnight", 6, 0), Create("whiteRock", 7, 0),
            Create("whitePawn", 0, 1), Create("whitePawn", 1, 1), Create("whitePawn", 2, 1), Create("whitePawn", 3, 1),
            Create("whitePawn", 4, 1), Create("whitePawn", 5, 1), Create("whitePawn", 6, 1), Create("whitePawn", 7, 1)
        };

        playerBlack = new GameObject[]
        {
            Create("blackRock", 0, 7), Create("blackKnight", 1, 7), Create("blackBishop", 2, 7), Create("blackQueen", 3, 7),
            Create("blackKing", 4, 7), Create("blackBishop", 5, 7), Create("blackKnight", 6, 7), Create("blackRock", 7, 7),
            Create("blackPawn", 0, 6), Create("blackPawn", 1, 6), Create("blackPawn", 2, 6), Create("blackPawn", 3, 6),
            Create("blackPawn", 4, 6), Create("blackPawn", 5, 6), Create("blackPawn", 6, 6), Create("blackPawn", 7, 6)
        };

        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.parent = figures;
        ChessMan cm = obj.GetComponent<ChessMan>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        ChessMan cm = obj.GetComponent<ChessMan>();

        position[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        position[x, y] = null;

    }

    public GameObject GetPosition(int x, int y)
    {
        return position[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if(x<0 || y<0 || x>= position.GetLength(0) || y >= position.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPalyer()
    {
        return currentPlayer;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "black")
            currentPlayer = "white";
        else
            currentPlayer = "black";
    }

    public void FixedUpdate()
    {
        if(gameOver==true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");

        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = playerWinner+" is winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<TextMeshProUGUI>().enabled = true;
    }
}

