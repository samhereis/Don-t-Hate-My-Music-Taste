using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Pooling
{
    public abstract class PoolingManagerBase<T> : ScriptableObject
    {
        [SerializeField] protected T _poolable;

        [SerializeField] protected Transform _parent;

        [SerializeField] protected Queue<T> _poolablesQueue = new Queue<T>();

        [SerializeField] protected List<T> _poolablesDequeued = new List<T>();

        protected virtual void Init()
        {
            if (_parent == null) _parent = new GameObject(name).transform;
        }

        public virtual void Clear()
        {
            _poolablesQueue.Clear();

            _poolablesDequeued.Clear();

            DestroyImmediate(_parent.gameObject);
        }

        public abstract Task<TimeSpan> Spawn(int quantity = 5, Transform parent = null);

        public abstract Task<T> PutOff(Transform position, Quaternion rotation);

        public abstract Task<T> PutOff(Vector3 position, Quaternion rotation);

        public abstract void PutIn(T poolable, float delay = 0);

        public abstract void PutInAll();
    }
}