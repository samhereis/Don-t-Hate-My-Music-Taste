using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunAim : MonoBehaviour
{
    // Aims a weapon

    private Vector3 _aimPosition, _initialPosition;

    public float AnimationDuration = 0.05f;

    private bool _aimed = false;

    private void OnEnable()
    {
        _aimPosition = GetComponent<GunData>().aimPosition;
        _initialPosition = GetComponent<GunData>().initialLocalPosition;
    }

    public void Aim(Transform aimData, bool aim)
    {
        if(aim == false)
        {
            aimData.DOLocalMove(_initialPosition, AnimationDuration);
            _aimed = false;
        }
        else
        {
            aimData.DOLocalMove(_aimPosition, AnimationDuration);
            _aimed = true;
        }
    }
}
