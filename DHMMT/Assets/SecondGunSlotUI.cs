using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondGunSlotUI : MonoBehaviour
{
    public static SecondGunSlotUI instance;

    [SerializeField] Color ActiveColor;

    [SerializeField] Color InactiveColor;

    [SerializeField] Image image;

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public void Activate()
    {
        FirstGunSlotUI.instance.Deactivate();
        image.color = ActiveColor;
    }

    public void Deactivate()
    {
        image.color = InactiveColor;
    }
}
