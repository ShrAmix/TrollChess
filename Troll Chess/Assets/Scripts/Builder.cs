using System.Resources;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Gameplay
{
    // Інтерфейс IBuilder з методом Init
    public interface IBuilder
    {
        public void Init(GenericGrid genericGrid, Transform parent);
    }

    public class Builder : MonoBehaviour, IBuilder
    {
        // Поля для зберігання префабу, сітки та батьківського об'єкта
        private GameObject _currentTowerPrefab;
        private int _currentCost;
        private GenericGrid _grid;
        private Transform _parent;


        // Реалізація методу Init з інтерфейсу IBuilder
        public void Init(GenericGrid genericGrid, Transform parent)
        {
            _grid = genericGrid;
            _parent = parent;
        }

        private void Update()
        {
            // Перевірка на натискання лівої кнопки миші
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                _grid.GetXY(worldPosition, out int x, out int y);

                // Перевірка
                if (CanBuild(x, y))
                {
                    
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Vector2 mousePosition = Input.mousePosition;
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                _grid.GetXY(worldPosition, out int x, out int y);

                // Перевірка 
                if (CanDestroy(x, y))
                {
                    
                }
            }
        }


        // функція для перевірки чи можна щось збудувати на даній клітинці
        private bool CanBuild(int x, int y)
        {
            return !_grid.IsOutsideBounds(x, y) && !_grid.IsRedCell(x, y) && _currentTowerPrefab != null;
        }
        // функція для перевірки чи можна щось знищити на даній клітинці
        private bool CanDestroy(int x, int y)
        {
            return !_grid.IsOutsideBounds(x, y) && _grid.GetValue(x, y).GridGameObject != null;
        }

        
    }
}
