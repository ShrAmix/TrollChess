using System.Collections.Generic;
using UnityEngine;

// ���� GenericGrid, �� ������ � ����������

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

// ������ �������� � ����, �� ������� �� ���� ���� �� ������
public class GenericGrid
{
    // ���� ��� ��������� ��������� ����
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector2 _origin;
    private IGridObject[,] _grid;
    // ����������� ��� ����������� ����
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

    // ����� ��� ������������ �������� ������� � ������ ��������� �������
    public void SetRedBoxes(List<Vector2Int> cells)
    {
        foreach (Vector2Int coordinates in cells)
        {
            SetRedCell(coordinates.x, coordinates.y, true);
        }
    }

    // ����� ��� ������������ �������� ������� � ������ �����
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

    // ����� ��� ��������, �� ����������� ���������� �� ������ ����
    public bool IsOutsideBounds(int x, int y)
    {
        return (x < 0 || y < 0) || (x >= _width || y >= _height);
    }

    // ����� ��� ��������� ������� ��������� �������
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize + _origin;
    }

    // ����� ��� ��������� ������ ������� � ������� �����������
    public Vector2 GetCenterWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * _cellSize + new Vector2(_cellSize / 2, _cellSize / 2) + _origin;
    }

    // ����� ��� ��������� ��������� ���� � ������� ���������
    public void GetXY(Vector2 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - _origin).x / _cellSize);
        y = Mathf.FloorToInt((worldPosition - _origin).y / _cellSize);
    }

    // ����� ��� ������������ �������� � ����
    public void SetValue(int x, int y, GameObject gameObject)
    {
        if (!IsOutsideBounds(x, y))
        {
            SetRedCell(x, y, gameObject != null);
            SetGameObject(x, y, gameObject);
        }
    }

    // �������������� ������ ��� ������������ �������� � ������������� ������� ���������
    public void SetValue(Vector2 worldPosition, GameObject gameObject)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, gameObject);
    }

    // ����� ��� ��������� �������� � ����
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

    // �������������� ������ ��� ��������� �������� � ������������� ������� ���������
    public IGridObject GetValue(Vector2 worldPosition)
    {
        GetXY(worldPosition, out int x, out int y);
        return GetValue(x, y);
    }

    // ����� ��� ��������, �� � ������� ��������
    public bool IsRedCell(int x, int y)
    {
        // ��������, �� ������� ������ � ��������
        return _grid[x, y].IsRedCell;
    }

    // ����� ��� ��������� ������� �� �������
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
