using DG.Tweening;
using Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class RenderSettingsManager : MonoBehaviour
    {
        private static Color _currentColor;
        public static Color currentColor { get => _currentColor; private set { _currentColor = value; onWorldColorChanged?.Invoke(_currentColor); } }

        public static Action<Color> onWorldColorChanged;

        [SerializeField] private List<Material> _sharedMaterialsToChangeColorOf;

        private async void OnEnable()
        {
            await AsyncHelper.Delay(1);

            RenderSettings.fogEndDistance = GetRandomFogDistance();

            _currentColor = new Color(GetRandomColorValue(), GetRandomColorValue(), GetRandomColorValue());

            RenderSettings.fogColor = _currentColor;

            foreach (var material in _sharedMaterialsToChangeColorOf)
            {
                await AsyncHelper.Delay(() => material.DOColor(_currentColor, 1));
            }
        }

        private float GetRandomColorValue()
        {
            return Random.Range(0f, 1f);
        }

        private float GetRandomFogDistance()
        {
            return Random.Range(70, 200);
        }
    }
}