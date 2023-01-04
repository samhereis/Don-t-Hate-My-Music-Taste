using UnityEngine;

namespace Gameplay.Camera
{
    public class EnemyCamera : MonoBehaviour
    {
        [SerializeField] private Transform _moveCameraTowards;

        void Awake()
        {
            transform.position = _moveCameraTowards.position;
        }
    }
}