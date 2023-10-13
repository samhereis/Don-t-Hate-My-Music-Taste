using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.Interaction
{
    [DisallowMultipleComponent]
    public class AnimateUIElement : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Ease _ease = Ease.OutBack;
        [SerializeField] private float _onOverScale = 1.1f;
        [SerializeField] private bool _randomize = false;
        [SerializeField] private Vector3 _positionOnHoverMin = new Vector3(-20, -20, -20);
        [SerializeField] private Vector3 _positionOnHoverMax = new Vector3(20, 20, 20);

        [SerializeField] private Vector3 _rotationOnHoverMin = new Vector3(-5, -5, -5);
        [SerializeField] private Vector3 _rotationOnHoverMax = new Vector3(5, 5, 5);

        [Header("Timing")]
        [SerializeField] private float _animationDuration = 0.5f;

        [Header("Components on hover")]
        [SerializeField] protected GameObject[] _gameObjectsToDisableOnHover;
        [SerializeField] protected GameObject[] _gameObjectsToEnableOnHover;

        [SerializeField] protected MonoBehaviour[] _componentsToDisableOnHover;
        [SerializeField] protected MonoBehaviour[] _componentsToEnableOnHover;

        [Header("Events")]
        [SerializeField] private AnimateButtonsEvents _events;

        private Vector3 GetPositionToSetOnHover()
        {
            if (_randomize == false) { return _positionOnHoverMin; }

            if (_positionOnHoverMin.z >= _positionOnHoverMax.x) { _positionOnHoverMin.x = _positionOnHoverMax.x - 1; }
            if (_positionOnHoverMin.x >= _positionOnHoverMax.x) { _positionOnHoverMin.x = _positionOnHoverMax.x - 1; }
            if (_positionOnHoverMin.y >= _positionOnHoverMax.x) { _positionOnHoverMin.x = _positionOnHoverMax.x - 1; }

            var x = Random.Range(_positionOnHoverMin.x, _positionOnHoverMax.x);
            var y = Random.Range(_positionOnHoverMin.y, _positionOnHoverMax.y);
            var z = Random.Range(_positionOnHoverMin.z, _positionOnHoverMax.z);

            return new Vector3(x, y, z);
        }

        private Vector3 GetRotataionToSetOnHover()
        {
            if (_randomize == false) { return _rotationOnHoverMin; }

            if (_rotationOnHoverMin.z >= _rotationOnHoverMax.x) { _rotationOnHoverMin.x = _rotationOnHoverMax.x - 1; }
            if (_rotationOnHoverMin.x >= _rotationOnHoverMax.x) { _rotationOnHoverMin.x = _rotationOnHoverMax.x - 1; }
            if (_rotationOnHoverMin.y >= _rotationOnHoverMax.x) { _rotationOnHoverMin.x = _rotationOnHoverMax.x - 1; }

            var x = Random.Range(_rotationOnHoverMin.x, _rotationOnHoverMax.x);
            var y = Random.Range(_rotationOnHoverMin.y, _rotationOnHoverMax.y);
            var z = Random.Range(_rotationOnHoverMin.z, _rotationOnHoverMax.z);

            return new Vector3(x, y, z);
        }

        private void Awake()
        {
            if (_target == null) { _target = transform; }
        }

        private void OnEnable()
        {
            OnExit();
        }

        private void OnDestroy()
        {
            _target?.DOKill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _target.DOScale(_onOverScale, _animationDuration).SetEase(_ease);
            _target.DOLocalMove(GetPositionToSetOnHover(), _animationDuration).SetEase(_ease);
            _target.DOLocalRotate(GetRotataionToSetOnHover(), _animationDuration).SetEase(_ease);

            OnHover();

            _events._onHover?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _events._onClick?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _target.DOScale(1, _animationDuration).SetEase(_ease);
            _target.DOLocalMove(Vector3.zero, _animationDuration).SetEase(_ease);
            _target.DOLocalRotate(Vector3.zero, _animationDuration).SetEase(_ease);

            OnExit();

            _events._onExit?.Invoke();
        }

        private void OnHover()
        {
            foreach (var obj in _gameObjectsToDisableOnHover)
            {
                obj?.SetActive(false);
            }

            foreach (var obj in _gameObjectsToEnableOnHover)
            {
                obj?.SetActive(true);
            }

            foreach (var component in _componentsToDisableOnHover)
            {
                if (component != null) { component.enabled = false; }
            }

            foreach (var component in _componentsToEnableOnHover)
            {
                if (component != null) { component.enabled = true; }
            }
        }

        private void OnExit()
        {
            foreach (var obj in _gameObjectsToDisableOnHover)
            {
                obj?.SetActive(true);
            }

            foreach (var obj in _gameObjectsToEnableOnHover)
            {
                obj?.SetActive(false);
            }

            foreach (var component in _componentsToDisableOnHover)
            {
                if (component != null) { component.enabled = true; }
            }

            foreach (var component in _componentsToEnableOnHover)
            {
                if (component != null) { component.enabled = false; }
            }
        }
    }

    [System.Serializable]
    internal class AnimateButtonsEvents
    {
        [SerializeField] internal UnityEvent _onHover = new UnityEvent();
        [SerializeField] internal UnityEvent _onClick = new UnityEvent();
        [SerializeField] internal UnityEvent _onExit = new UnityEvent();
    }
}