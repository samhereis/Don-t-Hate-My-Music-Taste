using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    //Use to detect main player near Enemy

    [SerializeField] private Collider[] _colliders;

    [SerializeField] private float _radius = 20;

    [SerializeField] private LayerMask _targetMask = 3;

    [SerializeField] private EnemyStates _enemyStates;

    private void Awake()
    {
        _enemyStates ??= GetComponent<EnemyStates>();
    }
}
