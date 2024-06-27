using UnityEngine;

public class ChessPieceMovement : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    public Transform bodyTransform;
    public SpriteRenderer spriteRenderer;
    private Vector2Int spNum;
    private ChessController controller;
    

    private void Start()
    {
        controller = FindObjectOfType<ChessController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spNum=new Vector2Int(spriteRenderer.sortingOrder, spriteRenderer.sortingOrder + 1);
    }
    void Update()
    {
        // ѕерев≥р€Їмо, чи натиснута л≥ва кнопка миш≥
        if (Input.GetMouseButtonDown(0))
        {
            // ќтримуЇмо позиц≥ю курсора в св≥тових координатах
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);

            // ѕерев≥р€Їмо, чи курсор натискаЇ на цей об'Їкт
            RaycastHit2D hit = Physics2D.Raycast(cursorWorldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // ќб'Їкт був натиснутий, починаЇмо перем≥щенн€
                isDragging = true;

                // «бер≥гаЇмо в≥дстань м≥ж центром об'Їкту та точкою курсора
                offset = gameObject.transform.position - cursorWorldPoint;
            }
        }

        // якщо миша утримуЇтьс€, перем≥щуЇмо об'Їкт
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
            

            // «адаЇмо нову позиц≥ю об'Їкта з урахуванн€м збереженоњ в≥дстан≥
            gameObject.transform.position = cursorWorldPoint + offset;
        }

        // якщо в≥дпускаЇмо кнопку миш≥, зак≥нчуЇмо перем≥щенн€
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            gameObject.transform.position = bodyTransform.position;
            
        }

        if (isDragging)
        {
            spriteRenderer.sortingOrder = spNum.y;
        }
        else
        {
            spriteRenderer.sortingOrder = spNum.x;
        }
    }

    public  void IsDragging(bool isDragging)
    {
        this.isDragging = isDragging;
    }

    public void MoveVerHor(bool infinityGo)
    {
        Vector2Int body = new Vector2Int((int)(bodyTransform.position.x + 0.5f), (int)(bodyTransform.position.y + 0.5f));

        Vector2Int westNorth = new Vector2Int((int)(bodyTransform.position.x + 3.5f), (int)(bodyTransform.position.y + 3.5f));
        Vector2Int eastNorth = new Vector2Int((int)(bodyTransform.position.x + 3.5f), (int)(bodyTransform.position.y + 3.5f));
        Vector2Int westSouth = new Vector2Int((int)(bodyTransform.position.x + 3.5f), (int)(bodyTransform.position.y + 3.5f));
        Vector2Int eastSouth = new Vector2Int((int)(bodyTransform.position.x + 3.5f), (int)(bodyTransform.position.y + 3.5f));

        for (int i = 0; i < 16; i++)
        {
            westNorth.x -= 1;
            westNorth.y += 1;

            eastNorth.x += 1;
            eastNorth.y += 1;

            westSouth.x -= 1;
            westSouth.y -= 1;

            eastSouth.x += 1;
            eastSouth.y -= 1;

            /*if (controller.tabelController._boardCells[westNorth.x, westNorth.y]!=null)
            {
                foreach (GameObject obj in controller.pieces)
                {
                    //if(piece)
                }
            }*/
        }

        /*foreach (GameObject obj in controller.tabelController._boardCells)
        {
            
        }*/
    }

    public void MoveDiagon(bool infinityGo)
    {

    }

    public void MoveSpecial(int[] path)
    {

    }



    public void MovePawn()
    {

    }

    public void MoveKing()
    {
    }
    public void MoveQueen()
    {
    }
    public void MoveRook()
    {
    }
    public void MoveBishop()
    {
    }

    
}
