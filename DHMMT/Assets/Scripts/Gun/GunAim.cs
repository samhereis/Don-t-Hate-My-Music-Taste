using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GunAim : MonoBehaviour
{
    Vector3 _aimPosition, _initialPosition;
    public float animationDuration = 0.05f;
    bool _aimed = false;
    private void OnEnable()
    {
        _aimPosition = GetComponent<GunData>().aimPosition;
        _initialPosition = GetComponent<GunData>().initialLocalPosition;
    }
    public void Aim(Transform aimData)
    {
        if(_aimed)
        {
            aimData.DOLocalMove(_initialPosition, animationDuration);
            _aimed = false;
        }
        else
        {
            aimData.DOLocalMove(_aimPosition, animationDuration);
            _aimed = true;
        }
    }
}
