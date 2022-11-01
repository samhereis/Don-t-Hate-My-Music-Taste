using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DragEvents : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Settings")]
        [SerializeField] private float _distanceToUpDownDragDetect = 50;
        [SerializeField] private float _distanceToRightLeftDragDetect = 100;

        [Header("Debug")]
        [SerializeField] private Vector2 _startPos;
        [SerializeField] private Vector2 _fingerPos;

        public Action onBeggingDrag;
        public Action onEndDrag;

        public Action onSwipeRight;
        public Action onSwipeLeft;
        public Action onSwipeUp;
        public Action onSwipeDown;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = eventData.position;
            onBeggingDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _fingerPos = eventData.position;
            CheckSwipe();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke();
        }

        private void CheckSwipe()
        {
            if (verticalMove() > _distanceToUpDownDragDetect && verticalMove() > horizontalValMove())
            {
                if (_startPos.y - _fingerPos.y > 0) OnSwipeDown(); else if (_startPos.y - _fingerPos.y < 0) OnSwipeUp();
            }
            else if (horizontalValMove() > _distanceToRightLeftDragDetect && horizontalValMove() > verticalMove())
            {
                if (_startPos.x - _fingerPos.x > 0) OnSwipeRight(); else if (_startPos.x - _fingerPos.x < 0) OnSwipeLeft();
                _fingerPos = _startPos;
            }
        }

        private float verticalMove()
        {
            return Mathf.Abs(_startPos.y - _fingerPos.y);
        }

        private float horizontalValMove()
        {
            return Mathf.Abs(_startPos.x - _fingerPos.x);
        }

        private void OnSwipeRight()
        {
            onSwipeRight?.Invoke();
        }

        private void OnSwipeLeft()
        {
            onSwipeLeft?.Invoke();
        }

        private void OnSwipeUp()
        {
            _startPos = _fingerPos;
            onSwipeUp?.Invoke();
        }

        private void OnSwipeDown()
        {
            _startPos = _fingerPos;
            onSwipeDown?.Invoke();
        }
    }
}