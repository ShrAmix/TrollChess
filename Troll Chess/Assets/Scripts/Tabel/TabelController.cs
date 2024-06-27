using UnityEngine;
using System.Collections;

public class TableController : MonoBehaviour
{
    public GameObject cellPrefab1; // ������ ������� ���� 1
    public GameObject cellPrefab2; // ������ ������� ���� 2
    public GameObject[,] boardArray = new GameObject[16, 16]; // ����� ��� ��������� ������� �����
    public Transform parent;

    private bool isMoving = false;
    [SerializeField] private int globalClam = 0;
    private int currentShift = 0;
    private int direction = 1; // 1 - �� ������, -1 - �� ������

    void Start()
    {
        CreateBoard();
    }

    void Update()
    {
        
    }

    void CreateBoard()
    {
        bool useCellPrefab1 = true;

        // ��������� �������� �����
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                GameObject cellPrefab = useCellPrefab1 ? cellPrefab1 : cellPrefab2;

                // ���������� ������ �� ������ ��� Z
                float distanceFromCenter = Mathf.Sqrt(Mathf.Pow(i - 7.5f, 2) + Mathf.Pow(j - 7.5f, 2));
                float zValue = 15f - distanceFromCenter; // 15 �� ����������� �������� Z

                GameObject cell = Instantiate(cellPrefab, new Vector3(j - 7.5f, i - 7.5f, zValue), Quaternion.identity, parent);
                boardArray[i, j] = cell;

                useCellPrefab1 = !useCellPrefab1;
            }
            useCellPrefab1 = !useCellPrefab1; // ��� ���� ������� �� ������ �����
        }
    }

    public void MoveBorders(int shiftAmount)
    {

        if (globalClam + shiftAmount <= 8 && globalClam + shiftAmount >= -8)
        {
            globalClam += shiftAmount;
        }
        else if (shiftAmount <= 0)
            shiftAmount = -8 - globalClam;
        else shiftAmount = 8 + globalClam;

        StartCoroutine(MoveBordersCoroutine(shiftAmount));
    }

    private IEnumerator MoveBordersCoroutine(int targetShift)
    {
        isMoving = true;
        targetShift= targetShift/2;
        if (targetShift < 0)
            direction = 1;
        else direction = -1;
        // �������� ��� �� �����
        while (currentShift != targetShift)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if ((j >= 0 && j <= 3) || (j >= 12 && j <= 15)) // ����� ��� ��� �� ����� �����
                    {
                        if (currentShift != targetShift)
                        {
                            if (j <= 3) // ˳�� ���� ������/����
                            {
                                boardArray[i, j].transform.position += new Vector3(2 * direction, 0, 0);
                            }
                            else if (j >= 12) // ����� ���� ����/������
                            {
                                boardArray[i, j].transform.position += new Vector3(-2 * direction, 0, 0);
                            }
                        }
                    }
                }
            }
            currentShift+=direction*-1;
            yield return new WaitForSeconds(0.1f); // �������� ��� �������� ����
        }

        // ���� ��� �� �����
        currentShift = 0;
        while (currentShift != targetShift)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if ((i >= 0 && i <= 3) || (i >= 12 && i <= 15)) // ����� ��� ������� �� ������ �����
                    {
                        if (currentShift != targetShift)
                        {
                            if (i <= 3) // ������ ���� ����/�����
                            {
                                boardArray[i, j].transform.position += new Vector3(0, 2 * direction, 0);
                            }
                            else if (i >= 12) // ����� ���� �����/����
                            {
                                boardArray[i, j].transform.position += new Vector3(0, -2 * direction, 0);
                            }
                        }
                    }
                }
            }
            currentShift+=direction*-1;
            yield return new WaitForSeconds(0.1f); // �������� ��� �������� ����
        }


        isMoving = false;
    }
}
