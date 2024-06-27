using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChessController : MonoBehaviour
{
    public GameObject[,] pieces;
    public GameObject[,] piecesTime;
    public GameObject piecePrefab;
    public Transform parent;
    //public TabelController tabelController;
    // Масиви для спрайтів білих і чорних фігур
    public Sprite[] whiteSprites;
    public Sprite[] blackSprites;

    

    private void Start()
    {
        ChessCreate();
    }
  
    

    public void ChessCreate()
    {
        // Ініціалізація фігур
        pieces = new GameObject[16, 16];
        piecesTime = new GameObject[16, 16];
        StawnChess(0, 8, 1, 1, 6, PieceType.Pawn);
        StawnChess(0, 8, 7, 0, 7, PieceType.Rook);
        StawnChess(1, 7, 5, 0, 7, PieceType.Bishop);
        StawnChess(2, 6, 3, 0, 7, PieceType.Knight);
        StawnChess(4, 5, 3, 0, 7, PieceType.King);
        StawnChess(3, 4, 3, 0, 7, PieceType.Queen);


        // Сортування об'єктів у parent за ім'ям
        SortPiecesByName();
    }

    
    void CreatePiece(PieceType type, PieceColor color, Vector2Int pos)
    {
        GameObject pieceObject = Instantiate(piecePrefab, transform.position, Quaternion.identity, parent);
        ChessPiece piece = pieceObject.GetComponent<ChessPiece>();



       // piece.cellTransf = tabelController._boardCells[pos.x+4,pos.y+4].transform;

        piece.chessController=this;

        piece.Initialize(type, color,  whiteSprites, blackSprites);
        
        pieceObject.name = $"{color}{type}";
        piece.positionArray = new Vector2Int(pos.x + 4, pos.y + 4);


        pieces[pos.x + 4, pos.y + 4]  = pieceObject;
        

        
    }





    public void StawnChess(int j, int max, int num, int y1, int y2, PieceType t)
    {
        for (int i = j; i < max; i += num)
        {
            CreatePiece(t, PieceColor.White, new Vector2Int(i, y1));
            CreatePiece(t, PieceColor.Black, new Vector2Int(i, y2));
        }
    }

    void SortPiecesByName()
    {
        // Отримуємо всі дочірні об'єкти батьківського об'єкту
        List<Transform> childTransforms = new List<Transform>();
        foreach (Transform child in parent)
        {
            childTransforms.Add(child);
        }

        // Сортуємо за ім'ям
        childTransforms.Sort((a, b) => string.Compare(a.name, b.name));

        // Переносимо відсортовані об'єкти в parent у правильному порядку
        for (int i = 0; i < childTransforms.Count; i++)
        {
            childTransforms[i].SetSiblingIndex(i);
        }
    }
    Vector3 PositionToVector3(Vector2Int position)
    {
        return new Vector3(position.x, 0, position.y);
    }
}
