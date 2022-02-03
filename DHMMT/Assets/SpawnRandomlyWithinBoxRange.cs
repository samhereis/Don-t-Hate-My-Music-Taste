using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Helpers;

namespace Identifiers
{
    public class SpawnRandomlyWithinBoxRange : MonoBehaviour
    {
        [SerializeField] private BoxCollider[] _boxColliders;

        private async void Awake()
        {
            await AsyncHelper.Delay(0.2f);

            Bounds bounds = _boxColliders[CollectionsHelper.GetRandomIndex(_boxColliders.Length)].bounds;

            float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
            float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
            float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

            transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {
            _boxColliders = GetComponents<BoxCollider>();
        }
#endif
    }
}