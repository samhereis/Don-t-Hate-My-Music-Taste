using System;
using DG.Tweening;
using Helpers;
using Sound;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Interaction
{
    public class BetterButton : Button, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SimpleSound _clickSoundResponce;
        [SerializeField] private ButtonAnimations _butonAnimations = new ButtonAnimations();

        protected override void Awake()
        {
            base.Awake();

            _butonAnimations.SetTransform(transform);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            _butonAnimations.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            _butonAnimations.OnPointerEnter(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (IsActive() && IsInteractable())
            {
                SoundPlayer.instance?.TryPlay(_clickSoundResponce);
            }
        }
    }

    [Serializable]
    public class ButtonAnimations
    {
        [SerializeField] private Transform _transform;

        [Header("Timing")]
        [SerializeField] private float _animationDuration = 0.25f;
        [SerializeField] private float _delayBetweenAnimations = 0;

        [Header("Settings")]
        [SerializeField] private float _onOverScale = 1.1f;
        [SerializeField] private float _normaleScale = 1;
        [SerializeField] private Ease _ease;

        public void SetTransform(Transform transform)
        {
            _transform = transform;
        }

        public async void OnPointerEnter(PointerEventData eventData)
        {
            await AsyncHelper.Delay(_delayBetweenAnimations);
            _transform.DOScale(_onOverScale, _animationDuration).SetEase(_ease);
        }

        public async void OnPointerExit(PointerEventData eventData)
        {
            await AsyncHelper.Delay(_delayBetweenAnimations);
            _transform.DOScale(_normaleScale, _animationDuration).SetEase(_ease);
        }
    }

# if UNITY_EDITOR
    [CustomEditor(typeof(BetterButton))]
    public class BetterButtonEditor : UnityEditor.UI.ButtonEditor
    {
        SerializedProperty _clickSoundResponce;
        SerializedProperty _butonAnimations;

        protected override void OnEnable()
        {
            base.OnEnable();
            _clickSoundResponce = serializedObject.FindProperty("_clickSoundResponce");
            _butonAnimations = serializedObject.FindProperty("_butonAnimations");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(_clickSoundResponce);
            serializedObject.ApplyModifiedProperties();

            serializedObject.Update();
            EditorGUILayout.PropertyField(_butonAnimations);
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}