using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Helpers
{
    public sealed class GridLayoutGroupHelper : MonoBehaviour
    {
        private enum GridLayoutGroupOffsetType { plus, mulpiply }
        private Action _updateAction;

        [Header("Componenets")]
        [SerializeField] private GridLayoutGroup _gridLayout;

        [Header("Settings")]
        [SerializeField] private GridLayoutGroupOffsetType _gridLayoutGroupOffsetType;
        [SerializeField] private float _horrizontalOffset = 0;
        [SerializeField] private float _verticalOffset = 0;

        [Header("Debug")]
        [SerializeField] private Vector2 _currentScreenSize;

        private void OnEnable()
        {
            if (_gridLayoutGroupOffsetType == GridLayoutGroupOffsetType.plus) _updateAction = SetGridLayoutSizes_Plus;
            else if (_gridLayoutGroupOffsetType == GridLayoutGroupOffsetType.mulpiply) _updateAction = SetGridLayoutSizes_Multiply;
        }

        private void Update()
        {
            _currentScreenSize = new Vector2(Screen.width, Screen.height);

            _updateAction?.Invoke();
        }

        private void SetGridLayoutSizes_Plus()
        {
            float horrizontal = _currentScreenSize.x + _horrizontalOffset;
            float vertical = _currentScreenSize.y + _verticalOffset;

            if (_horrizontalOffset == 0) horrizontal = _gridLayout.cellSize.x;
            if (_verticalOffset == 0) vertical = _gridLayout.cellSize.y;

            _gridLayout.cellSize = new Vector2(horrizontal, vertical);
        }

        private void SetGridLayoutSizes_Multiply()
        {
            float horrizontal = _currentScreenSize.x * _horrizontalOffset;
            float vertical = _currentScreenSize.y * _verticalOffset;

            if (_horrizontalOffset == 0) horrizontal = _gridLayout.cellSize.x;
            if (_verticalOffset == 0) vertical = _gridLayout.cellSize.y;

            _gridLayout.cellSize = new Vector2(horrizontal, vertical);
        }
    }
}