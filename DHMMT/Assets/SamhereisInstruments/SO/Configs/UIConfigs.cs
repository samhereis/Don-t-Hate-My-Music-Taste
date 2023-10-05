using AYellowpaper.SerializedCollections;
using DG.Tweening;
using SO;
using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UIConfigs", menuName = "Scriptables/Config/UIConfigs")]
    public class UIConfigs : ConfigBase
    {
        public const string orderText_SpritePlaceholderString = "orderSpriteHere";

        public static float defaultUIScaleAnimationDuration { get; private set; } = 0.5f;
        public static Ease defaultUIScaleEase { get; private set; } = Ease.OutBack;
        public static float defaultUIFadeAnimationDuration { get; private set; } = 0.5f;
        public static Ease defaultUIFadeEase { get; private set; } = Ease.OutBack;
        public static float defaultUIAnimationElementForeachDelay = 0.025f;

        [field: SerializeField, Tooltip("Время скейлинга UI элементов")] public float uiScaleAnimationDuration { get; private set; } = 0.25f;
        [field: SerializeField, Tooltip("Ease скейлинга UI элементов")] public Ease uiScaleEase { get; private set; } = Ease.InOutBack;
        [field: SerializeField, Tooltip("Время скейлинга UI элементов")] public float uiFadeAnimationDuration { get; private set; } = 0.25f;
        [field: SerializeField, Tooltip("Ease скейлинга UI элементов")] public Ease uiFadeEase { get; private set; } = Ease.InOutBack;
        [field: SerializeField, Tooltip("Ease скейлинга UI элементов")] public float uiAnimationElementForeachDelay { get; private set; } = 0.025f;

        [field: SerializeField, Tooltip("Settings menu configs")] public UIConfings_Settings settingsMenuConfigs { get; private set; } = new UIConfings_Settings();
        [field: SerializeField, Tooltip("Sceme select menu configs")] public UIConfings_ScemeSelectMenu scemeSelectMenuConfigs { get; private set; } = new UIConfings_ScemeSelectMenu();
        [field: SerializeField, Tooltip("Gameplay menu configs")] public UIConfings_GameplayMenu gameplayMenuConfigs { get; private set; } = new UIConfings_GameplayMenu();

        [SerializedDictionary("Color Name", ("Color Value"))]
        [field: SerializeField] public SerializedDictionary<ColorSetUnitString, Color> colorSetUnits = new SerializedDictionary<ColorSetUnitString, Color>();

        public override void Initialize()
        {

        }

        [Serializable]
        public class ColorSet
        {
            [SerializedDictionary("Color Name", ("Color Value"))]
            [field: SerializeField] public SerializedDictionary<ColorSetUnitString, Color> colorSetUnits = new SerializedDictionary<ColorSetUnitString, Color>();
        }
    }

    [Serializable]
    public class UIConfings_Settings
    {
        [field: SerializeField, Tooltip("Active tab indicator move speed")] public float activeTabIndicatorMoveSpeed { get; private set; } = 1;
        [field: SerializeField, Tooltip("Active tab indicator move ease")] public Ease activeTabIndicatorMoveEase { get; private set; } = Ease.InOutBack;
    }

    [Serializable]
    public class UIConfings_ScemeSelectMenu
    {
        [field: SerializeField, Tooltip("Delay when clicked on a play button in a scene unit")] public float onPlayRightAwayDelay { get; private set; } = 1;
    }

    [Serializable]
    public class UIConfings_GameplayMenu
    {
        [field: SerializeField, Tooltip("Delay when clicked on a play button in a scene unit")] public Color crosshairColor { get; private set; } = Color.cyan;
    }
}