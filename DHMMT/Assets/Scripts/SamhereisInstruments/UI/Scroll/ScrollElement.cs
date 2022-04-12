using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using DG.Tweening;
using UnityEngine.UI;

namespace Sripts
{
    public sealed class ScrollElement : MonoBehaviour
    {
        public static bool isInAction = false;

        private bool _canMove = false;
        private bool canMove { get => _canMove; set { _canMove = value; isInAction = value; } }

        private bool _isScrolling = false;
        public bool isScrolling { get => _isScrolling; set { _isScrolling = value; isInAction = value; } }

        [Header("Components")]
        [SerializeField] private ScrollSnapRect _scrollSnapRect;

        [Header("Settings")]
        [SerializeField] private float _enableScale = 1.2f;
        [SerializeField] private float _disableScale = 1f;
        [SerializeField] private DragEvents _dragEvents;
        [SerializeField] private Image _image;

        [Header("Debug")]
        [SerializeField] private Vector3 _initialPosition;

        [SerializeField] private float _start;
        [SerializeField] private float _startParentPosition;

        public Action onEnable;
        public Action onDisable;

        private void OnValidate()
        {
            _dragEvents = GetComponent<DragEvents>();
            _image = GetComponent<Image>();
        }

        private async void OnEnable()
        {
            RegisterEvents();

            await AsyncHelper.Delay(1000);

            if (_scrollSnapRect == null)
            {
                _scrollSnapRect = GetComponentInParent<ScrollSnapRect>();

                if (_scrollSnapRect == null)
                {
                    _scrollSnapRect = FindObjectOfType<ScrollSnapRect>();
                }
            }

            if(_scrollSnapRect != null) _scrollSnapRect.RegidterElement(this);
        }

        private void OnDisable()
        {
            DeregisterEvents();

            if (_scrollSnapRect != null) _scrollSnapRect.DeregidterElement(this);
        }

        private void Update()
        {
            if (canMove && _isScrolling == false)
            {
                transform.position = Input.mousePosition;
            }
        }

        private void RegisterEvents()
        {
            _dragEvents.onSwipeDown += SetCanMove;
            _dragEvents.onBeggingDrag += OnBegginLeftDrag;
            _dragEvents.onSwipeLeft += OnDragHorizontal;
            _dragEvents.onSwipeRight += OnDragHorizontal;

            _dragEvents.onEndDrag += OnEndDrag;
        }

        private void DeregisterEvents()
        {
            _dragEvents.onSwipeDown -= SetCanMove;
            _dragEvents.onBeggingDrag -= OnBegginLeftDrag;
            _dragEvents.onSwipeLeft -= OnDragHorizontal;
            _dragEvents.onSwipeRight -= OnDragHorizontal;

            _dragEvents.onEndDrag -= OnEndDrag;
        }

        private void SetCanMove()
        {
            canMove = true; 
            
            if(_initialPosition == Vector3.zero)
            {
                _initialPosition = transform.localPosition;
            }

            _dragEvents.onSwipeDown -= SetCanMove;
            _dragEvents.onSwipeLeft -= OnDragHorizontal;
            _dragEvents.onSwipeRight -= OnDragHorizontal;

            _dragEvents.onEndDrag += SetCantMove;
        }

        private void SetCantMove()
        {
            canMove = false;

            transform.DOLocalMove(_initialPosition, 1);
            
            _dragEvents.onSwipeDown += SetCanMove; 
            _dragEvents.onSwipeLeft += OnDragHorizontal;
            _dragEvents.onSwipeRight += OnDragHorizontal;
        }

        private void OnBegginLeftDrag()
        {
            _start = Input.mousePosition.x;

            _startParentPosition = transform.parent.position.x;
        }

        private void OnEndDrag()
        {
            canMove = false;
            isScrolling = false;
        }

        private void OnDragHorizontal()
        {
            if(canMove == false)
            {
                _isScrolling = true;
                transform.parent.DOMoveX(_startParentPosition + (Input.mousePosition.x - _start), 0);
            }
        }

        public void Enable()
        {
            transform.DOScale(_enableScale, 1f);

            onEnable?.Invoke();
        }

        public void Disable()
        {
            transform.DOScale(_disableScale, 1f);
            onDisable?.Invoke();
        }
    }
}