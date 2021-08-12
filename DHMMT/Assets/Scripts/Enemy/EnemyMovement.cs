using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref navMeshAgent, GetComponent<NavMeshAgent>());
        ExtentionMethods.SetWithNullCheck(ref animator, GetComponent<Animator>());
    }

    public void MoveTo(Transform moveTo, int speed)
    {
        navMeshAgent.speed = speed;
        animator.SetFloat("moveVelocityY", navMeshAgent.speed);
        navMeshAgent.SetDestination(moveTo.position);
    }
}
