using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class MapBarrier : MonoBehaviour
    {
        [SerializeField] private float _animationDuration = 0.5f;

        [Header("Scale Settings")]
        [SerializeField] private Vector3 _offScale = Vector3.zero;
        [SerializeField] private Vector3 _onScale = Vector3.one;

        private void Awake()
        {
            Disable();
        }

        public void Enable()
        {
            transform.DOKill();

            SetActive(true);
            transform.DOScale(_onScale, _animationDuration);
        }

        public void Disable()
        {
            transform.DOKill();

            transform.DOScale(_offScale, _animationDuration).OnComplete(() => { SetActive(false); }); ;
        }

        public void SetActive(bool activeStatus)
        {
            gameObject.SetActive(activeStatus);
        }
    }
}