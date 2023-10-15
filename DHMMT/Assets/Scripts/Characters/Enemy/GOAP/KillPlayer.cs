using Charatcers.Enemy;
using Charatcers.Player;
using Identifiers;
using Managers;
using SO.GOAP;
using UnityEngine;

namespace GOAP.Actions
{
    public class KillPlayer : GAction
    {
        [Header("Settings")]
        [SerializeField] private float _distanceToPlayer = 10;

        [Header("Components")]
        [SerializeField] private EnemyAgent _enemyAgent;
        [SerializeField] private EnemyIdentifier _enemyIdentifier;

        [Header("GOAP Strings")]
        [SerializeField] private GOAPStrings _isNearPlayer;

        [Header("Debug")]
        [SerializeField] private PlayerIdentifier _currentPlayer;

        private PlayerIdentifier GetPlayer()
        {
            if (_currentPlayer == null)
            {
                _currentPlayer = FindFirstObjectByType<PlayerIdentifier>(FindObjectsInactive.Include);
            }

            return _currentPlayer;
        }

        public override bool IsAchievable()
        {
            if (GetPlayer() == null) return false;

            return true;
        }

        public override bool TryBeggin()
        {
            if (baseSettings.target == null)
            {
                baseSettings.target = GetPlayer().gameObject;
            }

            return baseSettings.target != null;
        }

        public override bool TryComplete()
        {
            if (IsActionValid() == false)
            {
                OnActionFail();
                return false; 
            }

            var isNearTarget = IsNearPlayer(_distanceToPlayer);

            if (isNearTarget)
            {
                _enemyAgent.Stop();
                _enemyIdentifier.TryGet<EnemyController>()?.Shoot(_currentPlayer.transform.position);
            }
            else
            {
                baseSettings.localStates.RemoveState(_isNearPlayer);
                SetIsRunning(false);
            }

            return isNearTarget;
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

        public override bool IsCompleted()
        {
            var isPlayerDead = _currentPlayer.TryGet<PlayerHealth>().isAlive == false;

            if (isPlayerDead)
            {
                _enemyAgent.Stop();
            }

            return isPlayerDead;
        }

        private bool IsNearPlayer(float distanceToCheck)
        {
            var isNearPlayer = Vector3.Distance(transform.position, baseSettings.target.transform.position) <= distanceToCheck;

            return isNearPlayer;
        }
    }
}
