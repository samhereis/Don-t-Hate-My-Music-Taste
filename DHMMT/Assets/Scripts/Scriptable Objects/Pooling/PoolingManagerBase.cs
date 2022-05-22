using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Pooling
{
    public abstract class PoolingManagerBase<T> : ScriptableObject where T : Component
    {
        [SerializeField] protected T _poolable;
        [SerializeField] protected Transform _parent;
        [SerializeField] protected Queue<T> _poolablesQueue = new Queue<T>();
        [SerializeField] protected List<T> _poolablesDequeued = new List<T>();

        protected virtual void Init()
        {
            if (_parent == null) _parent = new GameObject(name).transform;
        }

        public virtual async Task<TimeSpan> Spawn(int quantity = 5, Transform parent = null)
        {
            if (parent != null) _parent = parent; else Init();

            DateTime start = DateTime.Now;

            for (int i = 0; i < quantity; i++)
            {
                T t = Instantiate(_poolable, parent);
                PutIn(t);
                await AsyncHelper.Delay();
            }

            TimeSpan time = DateTime.Now - start;

            return time;
        }

        public virtual void Clear()
        {
            _poolablesQueue.Clear();

            _poolablesDequeued.Clear();

            DestroyImmediate(_parent.gameObject);
        }

        public virtual async Task<T> PutOff(Transform position, Quaternion rotation)
        {
            return await PutOff(position.position, rotation);
        }

        public virtual async Task<T> PutOff(Transform position)
        {
            return await PutOff(position.position, Quaternion.identity);
        }
        public virtual async Task<T> PutOff(Vector3 position)
        {
            return await PutOff(position, Quaternion.identity);
        }

        public virtual async Task<T> PutOff(Vector3 position, Quaternion rotation)
        {
            T t;

            if (_poolablesQueue.Count < 1) await Spawn(5, _parent);

            t = _poolablesQueue.Dequeue();

            _poolablesDequeued.Add(t);

            t.transform.position = position;

            t.transform.rotation = rotation;

            t.gameObject.SetActive(true);

            return t;
        }

        public virtual async void PutIn(T poolable, float delay = 0)
        {
            await AsyncHelper.Delay(delay);

            if (poolable)
            {
                poolable.gameObject.SetActive(false);

                poolable.transform.SetParent(_parent);

                poolable.transform.position = Vector3.zero;

                poolable.transform.rotation = Quaternion.identity;

                _poolablesQueue.Enqueue(poolable);

                _poolablesDequeued.Remove(poolable);
            }
        }

        public virtual async void PutInAll()
        {
            foreach (var poolable in _poolablesDequeued)
            {
                await AsyncHelper.Delay(() => PutIn(poolable));
            }

            Clear();
        }
    }
}