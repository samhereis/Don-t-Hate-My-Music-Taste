using DG.Tweening;
using Helpers;
using Sripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollSnapRect : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _listValue = 100;

    [Header("Componenets")]
    [SerializeField] private List<SingleScrollElement> _buttons = new List<SingleScrollElement>();
    [SerializeField] private RectTransform _contentRect;
    [SerializeField] private Image _center;

    [Header("Degub")]
    [SerializeField] private ScrollElement _selectedButton;
    [SerializeField] private float _contentVector;
    [SerializeField] float _nearestPos;
    [SerializeField] float _distance;
    [SerializeField] private float _substractFromFirst;
    [SerializeField] private float _substractFromLast;
    [SerializeField] private float _minPos;
    [SerializeField] private float _maxPos;

    private float maxPos => _minPos = _buttons.Last()._position.y - _substractFromLast;
    private float minPos => _maxPos = _buttons.First()._position.y - _substractFromFirst;

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        var buttonList = new List<SingleScrollElement>();
    }

    private async void Init()
    {
        _buttons.Clear();

        var buttonList = new List<SingleScrollElement>();

        _contentVector = 0;

        foreach (var button in GetComponentsInChildren<ScrollElement>())
        {
            await AsyncHelper.Delay();
            try { _buttons.Add(new SingleScrollElement(button, -button.transform.localPosition)); } catch (Exception ex) { Debug.LogWarning(ex); };
        }

        foreach(var button in _buttons)
        {
            await AsyncHelper.Delay();
            button._position = -button._button.transform.localPosition;
        }

        _buttons.AddRange(buttonList);
    }

    private void Update()
    {
        if (_buttons.Count == 0 || _buttons == null) return;

        try
        {
            _nearestPos = float.MaxValue;

            foreach (var button in _buttons)
            {
                _distance = Vector3.Distance(_center.transform.position, button._button.transform.position);

                if (_distance < _nearestPos)
                {
                    _nearestPos = _distance;
                    _selectedButton = button._button;
                }
            }

            _contentVector = Mathf.Clamp(_contentVector, minPos, maxPos);

            _contentRect.DOAnchorPosY(_contentVector, 0.1f);

            foreach (var button in _buttons) if (_selectedButton == button._button) button._button.Enable(); else button._button.Disable();

        } catch (System.Exception ex) { Debug.LogWarning(ex); Init(); }
    }

    public void ListContent(int value)
    {
        _contentVector -= value * _listValue;
    }

    [ContextMenu(nameof(DebugView))] public void DebugView()
    {
        Init();

        Update();

        ListContent(1);
    }
}

[System.Serializable] internal class SingleScrollElement
{
    internal SingleScrollElement(ScrollElement sentButton, Vector2 sentPosition) { _button = sentButton; _position = sentPosition; }

    [SerializeField] internal ScrollElement _button;
    [SerializeField] internal Vector2 _position;
}