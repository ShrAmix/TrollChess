using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using Gameplay;


public class Starter : MonoBehaviour
{
    // Поле для зберігання префабу будівлі
    [SerializeField]
    private GameObject[] _cellPrefab;

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

    

    private Builder _builder;
    private int width=16, height=16;

    void Start()
    {
        

        // Ініціалізація сітки
        _origin.x = width/2*-1* _cellSize;
        _origin.y = height/2*-1* _cellSize;
        _grid = new GenericGrid(16, 16, _cellSize, _origin);

        // Додавання компонента Builder і його ініціалізація
        _builder = gameObject.AddComponent<Builder>();
        _builder.Init(_grid, _parent);

        // Перебір всіх клітинок у Tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        SetRedBox();
    }


    public void SetRedBox()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if(i< (width-_width)/2 ||
                    i >= _width+ (width - _width) / 2 ||
                    j < (height - _height)/2 ||
                    j >= _height+ (height - _height) / 2)
                    _grid.SetRedCell(i,j,true);
            }
        }
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
