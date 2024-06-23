using System.Resources;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Gameplay
{
    // ��������� IBuilder � ������� Init
    public interface IBuilder
    {
        public void Init(GenericGrid genericGrid, Transform parent);
    }

    public class Builder : MonoBehaviour, IBuilder
    {
        // ���� ��� ��������� �������, ���� �� ������������ ��'����
        private GameObject _currentTowerPrefab;
        private int _currentCost;
        private GenericGrid _grid;
        private Transform _parent;


        // ��������� ������ Init � ���������� IBuilder
        public void Init(GenericGrid genericGrid, Transform parent)
        {
            _grid = genericGrid;
            _parent = parent;
        }

        private void Update()
        {
            // �������� �� ���������� ��� ������ ����
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                _grid.GetXY(worldPosition, out int x, out int y);

                // ��������
                if (CanBuild(x, y))
                {
                    
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Vector2 mousePosition = Input.mousePosition;
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                _grid.GetXY(worldPosition, out int x, out int y);

                // �������� 
                if (CanDestroy(x, y))
                {
                    
                }
            }
        }


        // ������� ��� �������� �� ����� ���� ��������� �� ���� �������
        private bool CanBuild(int x, int y)
        {
            return !_grid.IsOutsideBounds(x, y) && !_grid.IsRedCell(x, y) && _currentTowerPrefab != null;
        }
        // ������� ��� �������� �� ����� ���� ������� �� ���� �������
        private bool CanDestroy(int x, int y)
        {
            return !_grid.IsOutsideBounds(x, y) && _grid.GetValue(x, y).GridGameObject != null;
        }

        
    }
}
