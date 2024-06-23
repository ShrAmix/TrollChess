using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Starter : MonoBehaviour
{
    // ���� ��� ��������� ������� �����
    [SerializeField]
    private GameObject[] _buildingPrefab;

    // ���� ��� ��������� ������������ ��'����
    [SerializeField]
    private Transform _parent;

    // ���� ��� ��������� ����
    private GenericGrid _grid;

    // ���� ��� ������������ ������ ����
    [SerializeField]
    private int _width;

    [SerializeField]
    private int _height;

    [SerializeField]
    private float _cellSize;

    // ���� ��� ��������� ��������� ������� ����
    [SerializeField] private Vector2 _origin;

    private List<Vector2> path;

    [SerializeField] private Tilemap tilemap;

    [SerializeField]
    private RectTransform _buttonContainer;
    

    private Builder _builder;
    

    void Start()
    {
        // ����������� ����
        _grid = new GenericGrid(_width, _height, _cellSize, _origin);

        // ��������� ���������� Builder � ���� �����������
        _builder = gameObject.AddComponent<Builder>();
        _builder.Init(_grid, _parent);

        // ������ ��� ������� � Tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);


    }


    public void SetRedCells(List<Vector2> points)
    {
        _grid.SetRedBoxes(points);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (_grid != null)
        {
            // ��������� ������� ����
            Vector2 size = new Vector2(_grid.GetCellSize(), _grid.GetCellSize());
            for (int x = 0; x < _grid.GetWidth(); x++)
            {
                for (int y = 0; y < _grid.GetHeight(); y++)
                {
                    Gizmos.DrawWireCube(_grid.GetCenterWorldPosition(x, y), size);
                }
            }

            // ��������� �������� �������
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
}
