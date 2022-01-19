using UnityEngine;
using UnityEngine.UI;

public class SecondGunSlotUI : MonoBehaviour
{
    // Second gun shower while gameplay

    public static SecondGunSlotUI instance;

    [SerializeField] private Color _activeColor;

    [SerializeField] private Color _inactiveColor;

    [SerializeField] private Image _image;

    private void Awake()
    {
        instance ??= this;
    }

    public void Activate()
    {
        FirstGunSlotUI.instance.Deactivate();
        _image.color = _activeColor;
    }

    public void Deactivate()
    {
        _image.color = _inactiveColor;
    }
}
