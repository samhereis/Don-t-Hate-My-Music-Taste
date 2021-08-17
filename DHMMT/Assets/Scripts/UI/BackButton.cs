using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public IUIPage page;

    public GameObject toEnable, ToDisable;

    public bool IsUIPage;

    [SerializeField] Button button;

    void OnEnable()
    {
        if (IsUIPage)
        {
            BackStatics.IsUIPage = IsUIPage;
            BackStatics.page = transform.parent.GetComponentInParent<IUIPage>();
        }
        else
        {
            BackStatics.IsUIPage = IsUIPage;
            BackStatics.page = null;
        }

        BackStatics.toEnable = toEnable;
        BackStatics.ToDisable = ToDisable;

        BackStatics.button = button;
    }
    public void Back()
    {
        BackStatics.GetBack();
    }
}
