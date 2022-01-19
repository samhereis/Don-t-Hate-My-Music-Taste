using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    [SerializeField] private Camera _uICamera;

    private void Awake()
    {
        if(_uICamera == null)
        {
            _uICamera = GetComponentInChildren<Camera>();
        }
    }

    public void SetEnabled(bool value)
    {

    }
}
