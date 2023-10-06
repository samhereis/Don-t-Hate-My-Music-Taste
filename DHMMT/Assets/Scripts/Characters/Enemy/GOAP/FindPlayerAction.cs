using Charatcers.Enemy;
using Identifiers;
using Managers;
using SO.GOAP;
using UnityEngine;

namespace GOAP.Actions
{
    public class FindPlayerAction : GAction
    {
        [Header("Settings")]
        [SerializeField] private float _minDistanceToPlayer = 5;
        [SerializeField] private float _maxDistanceToPlayer = 10;

        [Header("GOAP Strings")]
        [SerializeField] private GOAPStrings _isNearPlayer;

        [Header("Components")]
        [SerializeField] private EnemyIdentifier _enemyIdentifier;

        public override bool IsAchievable()
        {
            if (FindObjectOfType<PlayerIdentifier>() == null) return false;

            return true;
        }

        public override bool TryBeggin()
        {
            if (baseSettings.target == null)
            {
                baseSettings.target = FindObjectOfType<PlayerIdentifier>().gameObject;
            }

            if (baseSettings.target == null) { return false; }

            _enemyIdentifier.TryGet<EnemyAgent>().GoTo(baseSettings.target.transform.position);

            var shouldGoToTarget = ShouldGoToPlayer();

            if (shouldGoToTarget == false)
            {
                baseSettings.localStates.SetState(_isNearPlayer, 0);
                _enemyIdentifier.TryGet<EnemyAgent>().Stop();
            }

            return shouldGoToTarget;
        }

        public override bool TryComplete()
        {
            if (IsActionValid() == false)
            {
                OnActionFail();
                return false;
            }

            var isNearTarget = IsNearPlayer();

            if (isNearTarget)
            {
                _enemyIdentifier.TryGet<EnemyAgent>().Stop();
                baseSettings.localStates.SetState(_isNearPlayer, 0);
                SetIsRunning(false);
            }
            else
            {
                _enemyIdentifier.TryGet<EnemyAgent>().GoTo(baseSettings.target.transform.position);
            }

            return isNearTarget;
        }

        public override bool IsCompleted()
        {
            var isCompleted = IsNearPlayer();

            if (isCompleted)
            {
                _enemyIdentifier.TryGet<EnemyAgent>().Stop();
            }

            return isCompleted;
        }

        public override bool IsActionValid()
        {
            if (GlobalGameSettings.gameplayMode != GlobalGameSettings.GameplayMode.Gameplay)
            {
                return false;
            }

            if (baseSettings.target == null)
            {
                return false;
            }

            if (_enemyIdentifier.TryGet<EnemyHealth>()?.isAlive == false)
            {
                return false;
            }

            return base.IsActionValid();
        }

        public override void OnActionFail()
        {
            base.OnActionFail();

            _enemyIdentifier.TryGet<EnemyAgent>().Stop();
        }

        private bool ShouldGoToPlayer()
        {
            var shouldGoToPlayer = Vector3.Distance(transform.position, baseSettings.target.transform.position) >= _maxDistanceToPlayer;

            return shouldGoToPlayer;
        }

        private bool IsNearPlayer()
        {
            var isNearPlayer = Vector3.Distance(transform.position, baseSettings.target.transform.position) <= _minDistanceToPlayer;

            return isNearPlayer;
        }
    }
}