using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public IUIPage page;

    public GameObject toEnable, ToDisable;

    public bool IsUIPage;

    private void OnEnable()
    {
        if(IsUIPage)
        {
            page = transform.parent.GetComponentInParent<IUIPage>();
        }

        Back.OnBack = () => { GetBack(); };
    }

    void GetBack()
    {
        if(IsUIPage)
        {
            Back.OnBack = null;
            page.Disable();

            return;
        }

        toEnable.SetActive(false);
        ToDisable.SetActive(true);
    }
}
