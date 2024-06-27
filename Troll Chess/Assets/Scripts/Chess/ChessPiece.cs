using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    King,
    Queen,
    Rook,
    Bishop,
    Knight,
    Pawn
}

public enum PieceColor
{
    White,
    Black
}

public class ChessPiece : ChessPieceMovement
{
    public PieceType pieceType;
    public PieceColor pieceColor;
    public Vector2 position;
    public Vector2Int positionArray;
    public Transform cellTransf;
    // Масиви для спрайтів білих і чорних фігур

    public Sprite[] whiteSprites;
    public Sprite[] blackSprites;

    public ChessController chessController;


    public bool isFirstGO = true;



    private void Update()
    {
        CellTransform();
        /*if ((int)(position.x + 0.5f) % 2 == 0 && (int)(position.y + 0.5f) % 2 == 0)
        {
            GameObject ob1 = chessController.pieces[(int)cellTransf.position.x, (int)cellTransf.position.y];
            GameObject ob2 = chessController.pieces[positionArray.x, positionArray.y];

            chessController.pieces[positionArray.x, positionArray.y]=ob1;
            chessController.pieces[(int)cellTransf.position.x, (int)cellTransf.position.y]=ob2;

            //chessController.pieces[positionArray.x, positionArray.y] = null;
            positionArray.x = (int)(cellTransf.position.x);
            positionArray.y = (int)(cellTransf.position.y);
        }*/
    }
    private void CellTransform()
    {
        if (cellTransf != null)
        {

            Vector2 cellMove = new Vector2(cellTransf.position.x, cellTransf.position.y);
            Move(cellMove);
        }
        else if (cellTransf == null)
        {
            //cellTransf = chessController.tabelController._boardCells[(int)(position.x + 7.5f), (int)(position.y + 7.5f)].transform;
        }
        
    }
    public void Initialize(PieceType type, PieceColor color, Sprite[] whiteSpritesArray, Sprite[] blackSpritesArray)
    {
        pieceType = type;
        pieceColor = color;

        // Ініціалізація масивів спрайтів
        whiteSprites = whiteSpritesArray;
        blackSprites = blackSpritesArray;

        // Оновлення спрайта
        UpdateSprite();
    }

    void UpdateSprite()
    {
        Sprite[] spritesArray = pieceColor == PieceColor.White ? whiteSprites : blackSprites;
        string spriteName = pieceType.ToString();

        foreach (Sprite sprite in spritesArray)
        {
            if (sprite.name == spriteName)
            {
                spriteRenderer.sprite = sprite;
                break;
            }
        }
    }
    
    public void Move(Vector2 newPosition)
    {
        
            position = newPosition;
            gameObject.transform.position = new Vector2(position.x, position.y);
        float n = Mathf.Round(position.x*2)/2;
        if (Mathf.Abs(position.x - n) <= 0.015f)
            position.x = n;
        n = Mathf.Round(position.y * 2) / 2;
        if (Mathf.Abs(position.y - n) <= 0.015f)
            position.y = n;



    }
}
