using System.Collections.Generic;
using UnityEngine;

// Клас GenericGrid, що працює з генериками

public interface IGridObject
{
    public bool IsRedCell { get; set; }

    public GameObject GridGameObject { get; set; }
}

public class GridObject : IGridObject
{
    public bool IsRedCell { get; set; }
    public GameObject GridGameObject { get; set; }


    public GridObject()
    {
        IsRedCell = false;
    }
}

// забрав дженеріки у сітки, бо зрозумів що вони поки не потрібні
public class GenericGrid
{
    // Поля для зберігання параметрів сітки
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector2 _origin;
    private IGridObject[,] _grid;
    // Конструктор для ініціалізації сітки
    public GenericGrid(int width, int height, float cellSize, Vector2 origin)
    {
        _cellSize = cellSize;
        _width = width;
        _height = height;
        _origin = origin;
        _grid = new IGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _grid[x, y] = new GridObject();
            }
        }
    }

    // метод для встановлення червоних клітинок з набору координат клітинок
    public void SetRedBoxes(List<Vector2Int> cells)
    {
        foreach (Vector2Int coordinates in cells)
        {
            SetRedCell(coordinates.x, coordinates.y, true);
        }
    }

    // метод для встановлення червоних клітинок з набору точок
    public void SetRedBoxes(List<Vector2> points)
    {
        foreach (Vector2 vector in points)
        {
            GetXY(vector, out int x, out int y);
            SetRedCell(x, y, true);
        }
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }

    // Метод для перевірки, чи знаходяться координати за межами сітки
    public bool IsOutsideBounds(int x, int y)
    {
        return (x < 0 || y < 0) || (x >= _width || y >= _height);
    }

    // Метод для отримання світових координат клітинки
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize + _origin;
    }

    // Метод для отримання центру клітинки у світових координатах
    public Vector2 GetCenterWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize + new Vector2(_cellSize / 2, _cellSize / 2) + _origin;
    }

    // Метод для отримання координат сітки з світових координат
    public void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _origin).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _origin).y / _cellSize);
    }

    // Метод для встановлення значення у сітку
    public void SetValue(int x, int y, GameObject gameObject)
    {
        if (!IsOutsideBounds(x, y))
        {
            SetRedCell(x, y, gameObject != null);
            SetGameObject(x, y, gameObject);
        }
    }

    // Перевантаження методу для встановлення значення з використанням світових координат
    public void SetValue(Vector2 worldPosition, GameObject gameObject)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, gameObject);
    }

    // Метод для отримання значення з сітки
    public IGridObject GetValue(int x, int y)
    {
        if (!IsOutsideBounds(x, y))
        {
            return _grid[x, y];
        }
        else
        {
            return default;
        }
    }

    // Перевантаження методу для отримання значення з використанням світових координат
    public IGridObject GetValue(Vector2 worldPosition)
    {
        GetXY(worldPosition, out int x, out int y);
        return GetValue(x, y);
    }

    // Метод для перевірки, чи є клітинка червоною
    public bool IsRedCell(int x, int y)
    {
        // Перевірка, чи поточна клітина є червоною
        return _grid[x, y].IsRedCell;
    }

    // Метод щоб позначити клітинку як червону
    private void SetRedCell(int x, int y, bool value)
    {
        if (!IsOutsideBounds(x, y))
        {
            _grid[x, y].IsRedCell = value;
        }
    }

    private void SetGameObject(int x, int y, GameObject gameObject)
    {
        if (!IsOutsideBounds(x, y))
        {
            _grid[x, y].GridGameObject = gameObject;
        }
    }
}
