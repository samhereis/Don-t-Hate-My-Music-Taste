using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class AnimationStatics
{
    public static void NormalShake(Transform obj, float duration)
    {
        obj.DOShakePosition(duration, 10f, 10, 50, false, true).SetUpdate(true);
    }
}
