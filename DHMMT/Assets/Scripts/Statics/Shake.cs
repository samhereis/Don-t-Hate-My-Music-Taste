using UnityEngine;
using DG.Tweening;

public static class Shake
{
    public static void DoShake(Transform obj)
    {
        obj.DOShakePosition(2f, 10f, 10, 50, false, true);
    }
}
