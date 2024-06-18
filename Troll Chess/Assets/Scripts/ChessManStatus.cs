using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessManStatus : MonoBehaviour
{
    
    public ChessPieceStatuses chessPieces;
    public int firstMove=0;


    [System.Serializable]
    public class ChessPieceStatuses
    {
        public bool pawn;
        public bool knight;
        public bool bishop;
        public bool rock;
        public bool queen;
        public bool king;
    }

}
