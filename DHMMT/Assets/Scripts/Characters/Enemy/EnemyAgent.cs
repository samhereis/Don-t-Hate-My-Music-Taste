using GOAP;
using GOAP.GoapDataClasses;
using SO.GOAP;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Charatcers.Enemy
{
    public class EnemyAgent : GAgent
    {
        [Serializable]
        public class GoalView
        {
            [Serializable]
            public class SubGoalView
            {
                public GOAPStrings subgoal;
                public int cost;
            }

            public SubGoalView[] subgoal;
            public int priority;
        }

        [Serializable]
        public class LocalStateView
        {
            public GOAPStrings subgoal;
            public int cost;
        }

        [SerializeField] private List<GoalView> _goapGoals = new List<GoalView>();
        [SerializeField] private List<LocalStateView> _localStates = new List<LocalStateView>();

        [Header("Components")]
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyController _enemyController;

        [Header("Settings")]
        [SerializeField] private float _walkSpeed = 1.5f;
        [SerializeField] private float _runSpeed = 3f;

        [Header("Settings")]
        [SerializeField] private float _walkSpeed_Animator = 1.5f;
        [SerializeField] private float _runSpeed_Animator = 3f;

        protected override void Start()
        {
            base.Start();

            foreach (var goal in _goapGoals)
            {
                var subGoals = new Dictionary<GOAPStrings, int>();

                foreach (var subgoal in goal.subgoal)
                {
                    subGoals.Add(subgoal.subgoal, subgoal.cost);
                }

                baseSettings.goals.Add(new SubGoals(subGoals, false), goal.priority);
            }

            foreach (var state in _localStates)
            {
                baseSettings.localStates.SetState(state.subgoal, state.cost);
            }
        }

        public void GoTo(Vector3 position)
        {
            SetDestination(position, _walkSpeed, _walkSpeed_Animator);
        }

        public void RunTo(Vector3 position)
        {
            SetDestination(position, _runSpeed, _runSpeed_Animator);
        }

        private void SetDestination(Vector3 position, float speed, float animatorSpeed)
        {
            _enemyController.Sprint(animatorSpeed);

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(position);
            _navMeshAgent.speed = speed;
        }

        public void Stop()
        {
            _enemyController.Stop(0);

            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }
    }
}