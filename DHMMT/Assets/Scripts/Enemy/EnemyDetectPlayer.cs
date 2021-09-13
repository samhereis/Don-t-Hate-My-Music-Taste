using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    //Use to detect main player near Enemy

    private Collider[] _colliders;

    private float _radius = 20;

    public LayerMask TargetMask = 3;

    [SerializeField] private EnemyStates _enemyStates;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref _enemyStates, GetComponent<EnemyStates>());
    }

    private void FixedUpdate()
    {
        _colliders = Physics.OverlapSphere(transform.position, _radius, TargetMask);

        if (_colliders.Length != 0)
        {
            _enemyStates.FollowedEnemy = _colliders[0].transform;
        }
    }
}
