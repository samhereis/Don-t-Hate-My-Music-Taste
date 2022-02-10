using Characters.States.Data;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    [SerializeField] private Collider[] _colliders;

    [SerializeField] private float _radius = 20;

    [SerializeField] private LayerMask _targetMask = 3;

    [SerializeField] private EnemyStates _enemyStates;

    private void Awake()
    {
        _enemyStates ??= GetComponent<EnemyStates>();
    }
}
