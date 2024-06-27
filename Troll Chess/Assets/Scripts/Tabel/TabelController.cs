using UnityEngine;
using System.Collections;

public class TableController : MonoBehaviour
{
    public GameObject cellPrefab1; // Префаб клітинки типу 1
    public GameObject cellPrefab2; // Префаб клітинки типу 2
    public GameObject[,] boardArray = new GameObject[16, 16]; // Масив для зберігання клітинок дошки
    public Transform parent;

    private bool isMoving = false;
    [SerializeField] private int globalClam = 0;
    private int currentShift = 0;
    private int direction = 1; // 1 - до центру, -1 - від центру

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

        // Створення шахматної дошки
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                GameObject cellPrefab = useCellPrefab1 ? cellPrefab1 : cellPrefab2;

                // Розрахунок відстані від центру для Z
                float distanceFromCenter = Mathf.Sqrt(Mathf.Pow(i - 7.5f, 2) + Mathf.Pow(j - 7.5f, 2));
                float zValue = 15f - distanceFromCenter; // 15 це максимальне значення Z

                GameObject cell = Instantiate(cellPrefab, new Vector3(j - 7.5f, i - 7.5f, zValue), Quaternion.identity, parent);
                boardArray[i, j] = cell;

                useCellPrefab1 = !useCellPrefab1;
            }
            useCellPrefab1 = !useCellPrefab1; // Для зміни кольору на новому рядку
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
        // Спочатку рух по ширині
        while (currentShift != targetShift)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if ((j >= 0 && j <= 3) || (j >= 12 && j <= 15)) // Умови для лівої та правої стінок
                    {
                        if (currentShift != targetShift)
                        {
                            if (j <= 3) // Ліва стіна вправо/вліво
                            {
                                boardArray[i, j].transform.position += new Vector3(2 * direction, 0, 0);
                            }
                            else if (j >= 12) // Права стіна вліво/вправо
                            {
                                boardArray[i, j].transform.position += new Vector3(-2 * direction, 0, 0);
                            }
                        }
                    }
                }
            }
            currentShift+=direction*-1;
            yield return new WaitForSeconds(0.1f); // Затримка для плавного руху
        }

        // Потім рух по висоті
        currentShift = 0;
        while (currentShift != targetShift)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if ((i >= 0 && i <= 3) || (i >= 12 && i <= 15)) // Умови для верхньої та нижньої стінок
                    {
                        if (currentShift != targetShift)
                        {
                            if (i <= 3) // Верхня стіна вниз/вгору
                            {
                                boardArray[i, j].transform.position += new Vector3(0, 2 * direction, 0);
                            }
                            else if (i >= 12) // Нижня стіна вгору/вниз
                            {
                                boardArray[i, j].transform.position += new Vector3(0, -2 * direction, 0);
                            }
                        }
                    }
                }
            }
            currentShift+=direction*-1;
            yield return new WaitForSeconds(0.1f); // Затримка для плавного руху
        }


        isMoving = false;
    }
}
