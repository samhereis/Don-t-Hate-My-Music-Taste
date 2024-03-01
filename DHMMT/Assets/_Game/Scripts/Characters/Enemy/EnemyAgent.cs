using GOAP;
using GOAP.GoapDataClasses;
using SO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Charatcers.Enemy
{
    public class EnemyAgent : GOAPAgent
    {
        [Serializable]
        public class GoalView
        {
            [Serializable]
            public class SubGoalView
            {
                public GOAP_String_SO subgoal;
                public int cost;
            }

            public SubGoalView[] subgoal;
            public int priority;
        }

        [Serializable]
        public class LocalStateView
        {
            public GOAP_String_SO subgoal;
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
                var subGoals = new Dictionary<GOAP_String_SO, int>();

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
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(position);
            _navMeshAgent.speed = speed;
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
        }
    }
}