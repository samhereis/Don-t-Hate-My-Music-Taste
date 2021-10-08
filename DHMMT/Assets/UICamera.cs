using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public static UICamera instance;

    [SerializeField] private Camera _uICamera;

    private void Awake()
    {
        if(_uICamera == null)
        {
            _uICamera = GetComponentInChildren<Camera>();
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void SetEnabled(bool value)
    {
        StartCoroutine(SetEnabledCoroutine(value));
    }

    public IEnumerator SetEnabledCoroutine(bool value)
    {
        if(value == true)
        {
            yield return Wait.NewWaitRealTime(1);
        }

        _uICamera.enabled = value;
    }
}
