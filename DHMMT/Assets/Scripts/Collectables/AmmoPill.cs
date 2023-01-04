using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Pills
{
    public class AmmoPill : MonoBehaviour
    {
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private float _plusToHealth = 40;
        [SerializeField] private float _speed = 0.5f;

        private void OnEnable()
        {

        }

        private void FixedUpdate()
        {

        }

        private void OnTriggerEnter(Collider triggerEnteredObject)
        {

        }
    }
}