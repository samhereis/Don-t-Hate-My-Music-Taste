using ConstStrings;
using DataClasses;
using DG.Tweening;
using DI;
using Events;
using Identifiers;
using Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Charatcers.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamagable, IDIDependent
    {
        public Action onDie { get; set; }

        [field: SerializeField, Header("Debug")] public bool isAlive { get; private set; } = true;
        [field: SerializeField] public float currentHealth { get; private set; } = 100;
        [field: SerializeField] public float maxHealth { get; private set; } = 100;
        [field: SerializeField] public IdentifierBase damagedObjectIdentifier { get; private set; }

        [Header("DI")]
        [DI(Event_DIStrings.onEnemyDied)][SerializeField] private EventWithOneParameters<IDamagable> _onEnemyDied;

        [Header("Settings")]
        [SerializeField] private Gradient _healthGradient;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private EnemyAgent _enemyAgent;

        private Image _healthSliderFillRectImage;

        private void Awake()
        {
            damagedObjectIdentifier = GetComponent<IdentifierBase>();
            _enemyAgent = GetComponent<EnemyAgent>();
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            if (_healthSlider != null)
            {
                _healthSlider.maxValue = maxHealth;
                _healthSlider.value = currentHealth;

                _healthSliderFillRectImage = _healthSlider.fillRect.GetComponent<Image>();
            }

            UpdateHealthSlider();
        }

        private void OnDestroy()
        {
            if (_healthSlider != null)
            {
                _healthSlider.DOKill();
            }
        }

        public void TakeDamage(float damage, IDamagerObject damagerObject)
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
                aDamage.damagerActor = damagerObject.damagerActor;
                aDamage.damagerObject = damagerObject;
                aDamage.damagable = this;
                aDamage.damageAmount = damage;
                aDamage.healthAfterDamage = currentHealth;

                damagerObject.damagerActor.OnDamaged(aDamage);
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

            onDie?.Invoke();
            _onEnemyDied?.Invoke(this);

            isAlive = false;

            Destroy(gameObject, 5);
        }

        private void UpdateHealthSlider()
        {
            if (_healthSlider != null)
            {
                _healthSlider.DOKill();

                _healthSlider.transform.DOScaleY(1, 0.25f).OnComplete(() =>
                {
                    _healthSliderFillRectImage.DOColor(_healthGradient.Evaluate(currentHealth / maxHealth), 0.25f);

                    _healthSlider.DOValue(currentHealth, 0.25f).OnComplete(() =>
                    {
                        _healthSlider.transform.DOScaleY(0, 0.25f);
                    });
                });
            }

        }
    }
}