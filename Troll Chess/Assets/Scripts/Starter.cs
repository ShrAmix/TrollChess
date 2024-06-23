using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Starter : MonoBehaviour
{
    // Поле для зберігання префабу будівлі
    [SerializeField]
    private GameObject[] _buildingPrefab;

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
    [SerializeField] private Vector2 _origin;

    private List<Vector2> path;

    [SerializeField] private Tilemap tilemap;

    [SerializeField]
    private RectTransform _buttonContainer;
    

    private Builder _builder;
    

    void Start()
    {
        // Ініціалізація сітки
        _grid = new GenericGrid(_width, _height, _cellSize, _origin);

        // Додавання компонента Builder і його ініціалізація
        _builder = gameObject.AddComponent<Builder>();
        _builder.Init(_grid, _parent);

        // Перебір всіх клітинок у Tilemap
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
}
