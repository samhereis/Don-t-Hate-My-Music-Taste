using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    // Moves enemy to position. Controlled by other scripts

    public NavMeshAgent NavMeshAgentOfEnemy;
    public Animator AnimatorOfEnemy;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref NavMeshAgentOfEnemy, GetComponent<NavMeshAgent>());
        ExtentionMethods.SetWithNullCheck(ref AnimatorOfEnemy, GetComponent<Animator>());
    }

    public void MoveTo(Transform moveTo, int speed)
    {
        NavMeshAgentOfEnemy.speed = speed;
        AnimatorOfEnemy.SetFloat("moveVelocityY", NavMeshAgentOfEnemy.speed);
        NavMeshAgentOfEnemy.SetDestination(moveTo.position);
    }
}
