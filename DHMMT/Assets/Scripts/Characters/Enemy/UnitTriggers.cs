using Identifiers;
using System;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class UnitTriggers : MonoBehaviour
    {
        public Action<IdentifierBase> onEnter;
        public Action<IdentifierBase> onExit;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private SphereCollider _sphereCollider;

        private void OnValidate()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if (_sphereCollider == null) _sphereCollider = GetComponent<SphereCollider>();

            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;

            _sphereCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IdentifierBase identifierBase)) onEnter(identifierBase);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IdentifierBase identifierBase)) onExit(identifierBase);
        }
    }
}
