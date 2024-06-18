using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMan : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    public ChessManStatus status;

    //Position
    private int xBoard=-1;
    private int yBoard=-1;

    //White or black
    private string player;

    public Sprite blackQueen, blackKing, blackKnight, blackBishop, blackRock, blackPawn;
    public Sprite whiteQueen, whiteKing, whiteKnight, whiteBishop, whiteRock, whitePawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();
        switch (this.name)
        {
            case "blackQueen":  this.GetComponent<SpriteRenderer>().sprite = blackQueen; player = "black"; status.chessPieces.queen = true;      break;
            case "blackKing":   this.GetComponent<SpriteRenderer>().sprite = blackKing; player = "black"; status.chessPieces.king = true;        break;
            case "blackKnight": this.GetComponent<SpriteRenderer>().sprite = blackKnight; player = "black"; status.chessPieces.knight = true;    break;
            case "blackBishop": this.GetComponent<SpriteRenderer>().sprite = blackBishop; player = "black"; status.chessPieces.bishop = true;    break;
            case "blackRock":   this.GetComponent<SpriteRenderer>().sprite = blackRock; player = "black"; status.chessPieces.rock = true;        break;
            case "blackPawn":   this.GetComponent<SpriteRenderer>().sprite = blackPawn; player = "black"; status.chessPieces.pawn = true;        break;

            case "whiteQueen":  this.GetComponent<SpriteRenderer>().sprite = whiteQueen; player = "white"; status.chessPieces.queen = true;      break;
            case "whiteKing":   this.GetComponent<SpriteRenderer>().sprite = whiteKing; player = "white"; status.chessPieces.king = true;        break;
            case "whiteKnight": this.GetComponent<SpriteRenderer>().sprite = whiteKnight; player = "white"; status.chessPieces.knight = true;    break;
            case "whiteBishop": this.GetComponent<SpriteRenderer>().sprite = whiteBishop; player = "white"; status.chessPieces.bishop = true;    break;
            case "whiteRock":   this.GetComponent<SpriteRenderer>().sprite = whiteRock; player = "white"; status.chessPieces.rock = true;        break;
            case "whitePawn":   this.GetComponent<SpriteRenderer>().sprite = whitePawn; player = "white"; status.chessPieces.pawn = true;        break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.64f;
        y *= 0.64f;

        x += -2.08f;
        y += -2.84f;

        this.transform.position=new Vector3(x,y,0);
    }

    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPalyer() == player)
        {

            DestroyMovePlantes();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlantes()
    {
        GameObject[] movePlants = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlants.Length; i++)
        {
            Destroy(movePlants[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "blackQueen":
            case "whiteQueen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "blackKnight":
            case "whiteKnight":
                LMovePlate();
                break;
            case "blackBishop":
            case "whiteBishop":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                break;
            case "blackKing":
            case "whiteKing":
                SurroundMovePlate();
                break;
            case "blackRock":
            case "whiteRock":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "blackPawn":
                PawnMovePlate(xBoard, yBoard-1,-1);
                break;
            case "whitePawn":
                PawnMovePlate(xBoard, yBoard + 1,1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y)==null)
        {
            MovePlateSpawn(x,y,false);
            x += xIncrement;
            y += yIncrement;
        }
        if(sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<ChessMan>().player!=player)
        {
            MovePlateAttackSpawn(x,y);
        }
    }

    public void LMovePlate()
    {


        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);

    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (status.firstMove==0 && status.chessPieces.king==true && 
                sc.GetPosition(xBoard + 2, yBoard) == null && 
                sc.GetPosition(xBoard + 1, yBoard) == null)
            {
                GameObject cc = controller.GetComponent<Game>().GetPosition(xBoard + 3, yBoard);
                if (cc.GetComponent<ChessMan>().status.firstMove==0)
                MovePlateSpawn(xBoard + 2, yBoard, true);
            }
            if(status.firstMove==0 && status.chessPieces.king == true &&
                sc.GetPosition(xBoard - 1, yBoard) == null &&
                sc.GetPosition(xBoard - 2, yBoard) == null &&
                sc.GetPosition(xBoard - 3, yBoard) == null)
            {
                GameObject cc = controller.GetComponent<Game>().GetPosition(xBoard -4, yBoard);
                if (cc.GetComponent<ChessMan>().status.firstMove == 0)
                    MovePlateSpawn(xBoard - 2, yBoard, true);
            }

            if (cp == null)
            {
                MovePlateSpawn(x, y, false);
            }
            else if (cp.GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y,int bw)
    {
        Game sc = controller.GetComponent<Game>();
        
        if (sc.PositionOnBoard(x, y))
        {
            if(sc.GetPosition(x, y) == null)
            {
                if (status.firstMove == 0)
                {
                    MovePlateSpawn(x, y, false);
                    MovePlateSpawn(x, y + bw, false);
                }
                else
                {
                    MovePlateSpawn(x, y, false);
                }
            }
            if(sc.PositionOnBoard(x+1,y) && sc.GetPosition(x+1,y)!=null && 
                sc.GetPosition(x+1,y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x+1,y);
            }
            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<ChessMan>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }

        }
    }
    public void MovePlateSpawn(int matrixX, int matrixY, bool rocking)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.64f;
        y *= 0.64f;

        x += -2.08f;
        y += -2.84f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, 0), Quaternion.identity);
        if(status.firstMove==0 && status.chessPieces.king)
            if(rocking==true)
                mp.GetComponent<MovePlate>().kingRock = true;
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.64f;
        y *= 0.64f;

        x += -2.08f;
        y += -2.84f;

        GameObject mp = Instantiate(movePlate, new Vector3(x,y,-1f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
