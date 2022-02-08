using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent NavMeshAgentOfEnemy;
    public Animator AnimatorOfEnemy;

    private void Awake()
    {
        NavMeshAgentOfEnemy ??= GetComponent<NavMeshAgent>();
        AnimatorOfEnemy ??= GetComponent<Animator>();
    }

    public void MoveTo(Transform moveTo, int speed)
    {
        NavMeshAgentOfEnemy.speed = speed;
        AnimatorOfEnemy.SetFloat("moveVelocityY", NavMeshAgentOfEnemy.speed);
        NavMeshAgentOfEnemy.SetDestination(moveTo.position);
    }
}
