using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Gameplay;
using System;


public class TabelController : MonoBehaviour
{
    // Поле для зберігання префабу будівлі
    [SerializeField]
    private GameObject[] _cellPrefab;


    // Двумерный массив для хранения префабов
    public GameObject[,] _boardCells;
    public GameObject[,] _boardCellsTime;

    // Поле для зберігання батьківського об'єкта
    [SerializeField]
    private Transform _parent;

    // Поле для зберігання сітки
    private GenericGrid _grid;

    // Поля для налаштування розмірів сітки
    [SerializeField]
    private int _width;

    [SerializeField]
    private int _height;

    [SerializeField]
    private float _cellSize;

    // Поле для зберігання координат початку сітки
    //[SerializeField] 
    private Vector2 _origin;
    private Vector2 tap;

    private List<Vector2> path;

    [SerializeField] private Tilemap tilemap;

    

    private Builder _builder;
    private int width=-1, height=-1;
    private bool updateBoard = false;

    void Start()
    {
        // Ініціалізація сітки
        _origin.x = 16/2*-1* _cellSize;
        _origin.y = 16/2*-1* _cellSize;
        _grid = new GenericGrid(16, 16, _cellSize, _origin);

        // Додавання компонента Builder і його ініціалізація
        _builder = gameObject.AddComponent<Builder>();
        _builder.Init(_grid, _parent);

        // Инициализация массива для хранения префабов
        _boardCells = new GameObject[16, 16];
        _boardCellsTime = new GameObject[16, 16];

        // Перебір всіх клітинок у Tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

    }

    private void FixedUpdate()
    {
        if (_width != width || height != _height)
        {
            if (_height % 2 == 0 && _width % 2 == 0)
            {
                if (width != -1)
                {
                    tap.x = (_width > width) ? -Mathf.Abs(width - _width) / 2 : (_width < width) ? Mathf.Abs(width - _width) / 2 : 0;
                    tap.y = (_height > height) ? -Mathf.Abs(height - _height) / 2 : (_height < height) ? Mathf.Abs(height - _height) / 2 : 0;
                }
                else
                {
                    tap.x = (_width > width) ? -Mathf.Abs(width - 8) / 2 : (_width < width) ? Mathf.Abs(width - 8) / 2 : 0;
                    tap.y = (_height > height) ? -Mathf.Abs(height - 8) / 2 : (_height < height) ? Mathf.Abs(height - 8) / 2 : 0;
                }


                SetRedBox();

                width = _width;
                height = _height;
                
            }
        }

        UpdateBoard();
    }
    public void SetRedBox()
    {
        
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                _grid.SetAllWhiteCell(i,j);
                if (i < (16 - _width) / 2 || i >= _width + (16 - _width) / 2 ||
                    j < (16 - _height) / 2 || j >= _height + (16 - _height) / 2)
                {
                    _grid.SetRedCell(i, j, true);
                }
                else
                {
                    if((width == -1))
                        CreateTabel(i, j, true,false);
                    else if(Mathf.Abs((int)tap.x * 2)>0 || Mathf.Abs((int)tap.y * 2) > 0)
                    {
                        if (i >= 8 - Mathf.Abs((int)tap.x ) && i< 8 + Mathf.Abs((int)tap.x ))
                        {
                            CreateTabel(i,j,false,true);
                        }
                        else if(j >= 8 - Mathf.Abs((int)tap.y ) && j < 8 + Mathf.Abs((int)tap.y ))
                        {
                            CreateTabel(i, j, false,false);
                        }
                    }
                    
                }
                if (width != -1)
                {
                    MoveCell(i, j,tap);
                }

            }
        }
        
    }
    private void UpdateBoard()
    {
        if (updateBoard)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                // Отримуємо дочірній об'єкт
                Transform child = transform.GetChild(i);

                // Видаляємо дочірній об'єкт
                DestroyImmediate(child.gameObject);
            }
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    _boardCells[i, j] = null;
                    Destroy(_boardCells[i, j]);
                    _boardCellsTime[i, j] = null;
                    Destroy(_boardCellsTime[i, j]);
                    if (i >= (16 - _width) / 2 && i < _width + (16 - _width) / 2 &&
                        j >= (16 - _height) / 2 && j < _height + (16 - _height) / 2)
                        CreateTabel(i, j, true, false);
                }
            }
            updateBoard = false;
        }
       
    }
    public void CreateTabel(int x, int y, bool save, bool isX)
    {
        // Визначаємо який префаб використовувати
        int prefabIndex;
        if((((16 - _width) / 2)+((16 - _height) / 2))%2==0)
            prefabIndex = (x + y) % 2;
        else
            prefabIndex = (x + y+1) % 2;
        GameObject prefab = _cellPrefab[prefabIndex];
        Vector2 position = _grid.GetWorldPosition(x, y) + new Vector2(_cellSize, _cellSize) * 0.5f;

        if (_boardCells[x, y] == null && save)
        {
            // Створюємо префаб
            GameObject cell = Instantiate(prefab, position, Quaternion.identity, _parent);
            cell.transform.localScale = new Vector3(_cellSize, _cellSize, 1f);

            // Сохраняем префаб в двумерный массив
            _boardCells[x, y] = cell;
           
        }
        else if(!save)
        {


            // Створюємо префаб
            GameObject cell = Instantiate(prefab, position, Quaternion.identity, _parent);
            cell.transform.localScale = new Vector3(_cellSize, _cellSize, 1f);
            cell.GetComponent<SpriteRenderer>().sortingOrder = 1;

            _boardCellsTime[x, y] = cell;
        }
    }
    // Виклик функції для пересування
    public void MoveCell(int i, int j, Vector2 tap)
    {
        if (_boardCells[i, j] != null)
        {
            Vector3 targetPosition = _boardCells[i, j].transform.position;

            if (j < 8)
            {
                targetPosition.y += tap.y;
            }
            else if (j >= 8)
            {
                targetPosition.y -= tap.y;
            }

            if (i < 8)
            {
                targetPosition.x += tap.x;
            }
            else if (i >= 8)
            {
                targetPosition.x -= tap.x;
            }

            StartCoroutine(SmoothMove(_boardCells[i, j].transform, targetPosition, 1f,() => updateBoard = true)); // Виклик корутини
            
        }
    }
    // Корутину для плавного пересування
    private IEnumerator SmoothMove(Transform objTransform, Vector3 targetPosition, float duration, Action onComplete)
    {
        Vector3 startPosition = objTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            objTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Повертає виконання на наступний кадр
        }

        objTransform.position = targetPosition; // Установити кінцеву позицію

        onComplete?.Invoke(); // Виконати колбек після завершення анімації
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (_grid != null)
        {
            // Малювання контурів сітки
            Vector2 size = new Vector2(_grid.GetCellSize(), _grid.GetCellSize());
            for (int x = 0; x < _grid.GetWidth(); x++)
            {
                for (int y = 0; y < _grid.GetHeight(); y++)
                {
                    Gizmos.DrawWireCube(_grid.GetCenterWorldPosition(x, y), size);
                }
            }

            // Малювання червоних клітинок
            Gizmos.color = Color.red;

            for (int x = 0; x < _grid.GetWidth(); x++)
            {
                for (int y = 0; y < _grid.GetHeight(); y++)
                {
                    if (_grid.IsRedCell(x, y))
                    {
                        Gizmos.DrawWireCube(_grid.GetCenterWorldPosition(x, y), size);

                    }
                }
            }
        }
    }


    // Властивості для отримання та встановлення значень
    public int Width
    {
        get { return _width; }
        set { _width = RoundToNearestEven(value); }
    }

    public int Height
    {
        get { return _height; }
        set { _height = RoundToNearestEven(value); }
    }

    // Метод для округлення до найближчого парного числа з урахуванням обмежень
    private int RoundToNearestEven(int value)
    {
        // Обмежуємо значення між 8 і 16
        value = Mathf.Clamp(value, 8, 16);

        // Округляємо до найближчого парного числа
        return (value % 2 == 0) ? value : value + 1;
    }

    // Визиваєтся при змінні значення в інспекторі
    private void OnValidate()
    {
        Width = _width;
        Height = _height;
    }
}
