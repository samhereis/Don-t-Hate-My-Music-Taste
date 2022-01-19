using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstGunSlotUI : MonoBehaviour
{
    // First gun shower while gameplay

    public static FirstGunSlotUI instance;

    [SerializeField] private Color _activeColor;
                    
    [SerializeField] private Color _inactiveColor;
                    
    [SerializeField] private Image _image;

    void Awake()
    {
         instance ??= this;
    }

    public void Activate()
    {
        SecondGunSlotUI.instance.Deactivate();
        _image.color = _activeColor;
    }

    public void Deactivate()
    {
        _image.color = _inactiveColor;
    }
}
