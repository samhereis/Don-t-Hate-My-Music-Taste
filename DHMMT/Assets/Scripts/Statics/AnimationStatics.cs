using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class AnimationStatics
{
    // Animations

    public static void NormalShake(Transform obj, float duration)
    {
        obj.DOShakePosition(duration, 10f, 10, 50, false, true).SetUpdate(true);
    }

    public static void NormalShake(Transform obj, float duration, float strenght)
    {
        obj.DOShakePosition(duration, strenght, 10, 50, false, true).SetUpdate(true);
    }
}
