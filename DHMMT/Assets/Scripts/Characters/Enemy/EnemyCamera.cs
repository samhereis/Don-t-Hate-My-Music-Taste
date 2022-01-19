using UnityEngine;

public class EnemyCamera : MonoBehaviour
{
    // Used to move enemie's camera to certain position on Awake

    [SerializeField] private Transform _moveCameraTowards;

    void Awake()
    {
        transform.position = _moveCameraTowards.position;
    }
}