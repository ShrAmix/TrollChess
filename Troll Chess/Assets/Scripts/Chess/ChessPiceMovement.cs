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
        // ����������, �� ��������� ��� ������ ����
        if (Input.GetMouseButtonDown(0))
        {
            // �������� ������� ������� � ������� �����������
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);

            // ����������, �� ������ ������� �� ��� ��'���
            RaycastHit2D hit = Physics2D.Raycast(cursorWorldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // ��'��� ��� ����������, �������� ����������
                isDragging = true;

                // �������� ������� �� ������� ��'���� �� ������ �������
                offset = gameObject.transform.position - cursorWorldPoint;
            }
        }

        // ���� ���� ����������, ��������� ��'���
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
            Vector3 cursorWorldPoint = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
            

            // ������ ���� ������� ��'���� � ����������� ��������� ������
            gameObject.transform.position = cursorWorldPoint + offset;
        }

        // ���� ��������� ������ ����, �������� ����������
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
