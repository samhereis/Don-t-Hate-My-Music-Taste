using DG.Tweening;
using Gameplay;
using Identifiers;
using UnityEngine;

namespace Characters.States.Attack
{
    public class DistantAttack : EnemyAttackBase
    {
        [SerializeField] private UnitTriggers _unitTriggers;

        [field: SerializeField] private EnemyWeapon _enemyWeapon;

        private void OnValidate()
        {
            Setup();
        }

        private void OnEnable()
        {
            Setup();

            _unitTriggers.onEnter += OnEnter;
            _unitTriggers.onExit += OnExit;
        }

        private void OnDisable()
        {
            _unitTriggers.onEnter -= OnEnter;
            _unitTriggers.onExit -= OnExit;
        }

        private void OnEnter(IdentifierBase other)
        {
            if (other is PlayerIdentifier)
            {
                if (_targets.Contains(other) == false) _targets.Add(other);
            }
        }

        private void OnExit(IdentifierBase other)
        {
            if (other is PlayerIdentifier)
            {
                if (_targets.Contains(other)) _targets.Remove(other);
            }
        }

        private void FixedUpdate()
        {
            if (CanAttack()) Attack();
        }

        public override void Attack()
        {
            _enemyWeapon.transform.DOLookAt(_targets[0].transform.position, 0.25f);
            _enemyWeapon.currentWeapon.SetShoot(true);
        }

        public override bool CanAttack()
        {
            return _targets.Count > 0;
        }

        private void Setup()
        {
            if (_unitTriggers == null) _unitTriggers = GetComponentInParent<UnitTriggers>();
            if (_enemyWeapon == null) _enemyWeapon = GetComponentInParent<EnemyWeapon>();
        }
    }
}