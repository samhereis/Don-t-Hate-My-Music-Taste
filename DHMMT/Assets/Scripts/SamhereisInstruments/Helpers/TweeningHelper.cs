using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace Helpers
{
    public static class TweeningHelper
    {

        #region Transform
        public static Tweener NormalShake(this Transform obj, float duration, float strenght = 10)
        {
            return obj.DOShakePosition(duration, strenght, 10, 50, false, true).SetUpdate(true);
        }

        public static Tweener ScaleDown(this Transform obj, float duration, Ease ease = Ease.OutBack)
        {
            return obj.ScaleTo(0, duration, ease);
        }

        public static Tweener ScaleTo(this Transform obj, float value, float duration, Ease ease = Ease.OutBack)
        {
            return obj.DOScale(value, duration).SetEase(ease).SetUpdate(true);
        }

        public static Tweener ScaleUp(this Transform obj, float duration, Ease ease = Ease.OutBack)
        {
            return obj.ScaleTo(1, duration, ease);
        }

        #endregion

        #region Number

        public static TweenerCore<float, float, FloatOptions> TweenFloat(this float value, float to, float duration, Action<float> onUpdateCallback = null)
        {
            return DOTween.To(() => value, x => value = x, to, duration).OnUpdate(() => { onUpdateCallback?.Invoke(value); }).SetUpdate(true); ;
        }

        public static TweenerCore<int, int, NoOptions> TweenInt(this int value, int to, float duration, Action<int> onUpdateCallback = null)
        {
            return DOTween.To(() => value, x => value = x, to, duration).OnUpdate(() => { onUpdateCallback?.Invoke(value); }).SetUpdate(true);
        }

        #endregion

        #region

        public static Tweener FadeDown(this CanvasGroup obj, float duration = 0.5f, bool setUninteractable = true, Ease ease = Ease.OutBack, Action completeCallback = null)
        {
            if (obj == null) return null;

            if (setUninteractable)
            {
                obj.interactable = false;
                obj.blocksRaycasts = false;
            }
            return obj.FadeeTo(0, duration, ease).OnComplete(() =>
            {
                completeCallback?.Invoke();
            }); ;
        }

        public static Tweener FadeeTo(this CanvasGroup obj, float value, float duration, Ease ease = Ease.OutBack)
        {
            obj.DOKill();
            
            return obj.DOFade(value, duration).SetEase(ease).SetUpdate(true);
        }

        public static Tweener FadeUp(this CanvasGroup obj, float duration = 0.5f, bool setInteractable = true, Ease ease = Ease.OutBack, Action completeCallback = null)
        {
            if (obj == null) return null;
            
            if (setInteractable)
            {
                obj.interactable = true;
                obj.blocksRaycasts = true;
            }

            return obj.FadeeTo(1, duration, ease).OnComplete(() =>
            {

                completeCallback?.Invoke();
            });
        }

        #endregion
    }
}
