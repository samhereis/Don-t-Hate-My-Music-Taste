using ConstStrings;
using DataClasses;
using DependencyInjection;
using DG.Tweening;
using Identifiers;
using Interfaces;
using Observables;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Charatcers.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamagable, INeedDependencyInjection
    {
        public Action onDie { get; set; }

        [field: SerializeField, Header("Debug")] public bool isAlive { get; private set; } = true;
        [field: SerializeField] public float currentHealth { get; private set; } = 100;
        [field: SerializeField] public float maxHealth { get; private set; } = 100;
        [field: SerializeField] public IdentifierBase damagedGameobject { get; private set; }

        [Header("DI")]
        [Inject(DataSignal_ConstStrings.onEnemyDied)][SerializeField] private DataSignal<IDamagable> _onEnemyDied;

        [Header("Settings")]
        [SerializeField] private Gradient _healthGradient;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private EnemyAgent _enemyAgent;
        [SerializeField] private Target _targetIndicator;

        private Image _healthSliderFillRectImage;
        private MainCamera_Identifier _mainCamera_;

        private MainCamera_Identifier _mainCamera
        {
            get
            {
                if (_mainCamera_ == null)
                {
                    _mainCamera_ = FindFirstObjectByType<MainCamera_Identifier>(FindObjectsInactive.Include);
                }

                return _mainCamera_;
            }
        }

        private void Awake()
        {
            if (damagedGameobject == null) damagedGameobject = GetComponent<IdentifierBase>();
            if (_enemyAgent == null) _enemyAgent = GetComponent<EnemyAgent>();
            if (_targetIndicator == null) _targetIndicator = GetComponentInChildren<Target>(true);
        }

        private void Start()
        {
            DependencyContext.diBox.InjectDataTo(this);

            if (_healthSlider != null)
            {
                _healthSlider.maxValue = maxHealth;
                _healthSlider.value = currentHealth;

                _healthSliderFillRectImage = _healthSlider.fillRect.GetComponent<Image>();
            }


            UpdateHealthSlider(0);
        }

        private void OnDestroy()
        {
            if (_healthSlider != null)
            {
                _healthSlider.DOKill();
            }
        }

        public void TakeDamage(float damage, IDamager damagerObject)
        {
            if (isAlive == true)
            {
                currentHealth -= damage;

                if (currentHealth <= 0)
                {
                    Die();
                }

                UpdateHealthSlider();

                var aDamage = new ADamage();
                aDamage.damagerObject = damagerObject;
                aDamage.damagedObject = this;
                aDamage.damageAmount = damage;
                aDamage.healthAfterDamage = currentHealth;

                damagerObject.damagerActor.OnHasDamaged(aDamage);
            }
        }

        public void Die()
        {
            _enemyAgent?.Stop();
            _animator.enabled = false;

            foreach (var weapon in _animator.GetComponentsInChildren<WeaponIdentifier>(true))
            {
                weapon.gameObject.SetActive(false);
            }

            foreach (var monoBeh in _animator.GetComponentsInChildren<MonoBehaviour>(true))
            {
                monoBeh.enabled = false;
            }

            foreach (var rigidBody in _animator.GetComponentsInChildren<Rigidbody>(true))
            {
                rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rigidBody.isKinematic = false;
            }

            foreach (var characterJoint in _animator.GetComponentsInChildren<CharacterJoint>(true))
            {
                characterJoint.enableProjection = true;
            }

            isAlive = false;
            _targetIndicator?.gameObject.SetActive(false);
            Destroy(gameObject, 5);

            onDie?.Invoke();
            _onEnemyDied?.Invoke(this);
        }

        private void UpdateHealthSlider(float duration = 0.25f)
        {
            if (_healthSlider != null)
            {
                _healthSlider.DOKill();

                if (duration <= 0)
                {
                    _healthSliderFillRectImage.color = _healthGradient.Evaluate(currentHealth / maxHealth);
                    _healthSlider.value = currentHealth;
                    _healthSlider.transform.localScale = new Vector3(1, 0, 1);
                }
                else
                {
                    if (_mainCamera != null) { _healthSlider.transform.DOLookAt(_mainCamera.transform.position, duration); }

                    _healthSlider.transform.DOScaleY(1, duration).OnComplete(() =>
                    {
                        _healthSliderFillRectImage.DOColor(_healthGradient.Evaluate(currentHealth / maxHealth), duration);

                        _healthSlider.DOValue(currentHealth, duration).OnComplete(() =>
                        {
                            _healthSlider.transform.DOScaleY(0, duration);
                        });
                    });
                }
            }
        }
    }
}