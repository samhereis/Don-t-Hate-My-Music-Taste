using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace Helpers
{
    public static class TweeningHelper
    {
        public static Tweener NormalShake(Transform obj, float duration, float strenght = 10)
        {
            return obj.DOShakePosition(duration, strenght, 10, 50, false, true).SetUpdate(true);
        }

        public static Tweener ScaleDown(Transform obj, float duration, Ease ease = Ease.OutBack)
        {
            return obj.DOScale(0, duration).SetEase(ease);
        }

        public static Tweener ScaleUp(Transform obj, float duration, Ease ease = Ease.OutBack)
        {
            return obj.DOScale(0, duration).SetEase(ease);
        }

        public static TweenerCore<float, float, FloatOptions> TweenFloat(float value, float to, float duration, Action<float> onUpdateCallback = null)
        {
            return DOTween.To(() => value, x => value = x, to, duration).OnUpdate(() =>
            {
                onUpdateCallback?.Invoke(value);
            });
        }

        public static TweenerCore<int, int, NoOptions> TweenInt(int value, int to, float duration, Action<int> onUpdateCallback = null)
        {
            return DOTween.To(() => value, x => value = x, to, duration).OnUpdate(() =>
            {
                onUpdateCallback?.Invoke(value);
            });
        }
    }
}
