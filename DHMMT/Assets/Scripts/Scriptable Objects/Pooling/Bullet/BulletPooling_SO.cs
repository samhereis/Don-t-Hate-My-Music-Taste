using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Pooling
{
    [CreateAssetMenu(fileName = "New Bullet Pooler", menuName = "Scriptables/Pooling Managers/Bullet Pooler")]
    public class BulletPooling_SO : PoolingManagerBase<Bullet>
    {
        public override async Task<TimeSpan> Spawn(int quantity = 5, Transform parent = null)
        {
            DateTime start = DateTime.Now;

            if (parent) _parent = parent; else Init();

            for (int i = 0; i < quantity; i++)
            {
                Bullet bulletTemp = Instantiate(_poolable, parent);

                PutIn(bulletTemp);

                await AsyncHelper.Delay();
            }

            TimeSpan time = DateTime.Now - start;

            return time;
        }

        public override async Task<Bullet> PutOff(Transform position, Quaternion rotation)
        {
            return await PutOff(position.position, rotation);
        }

        public override async Task<Bullet> PutOff(Vector3 position, Quaternion rotation)
        {
            Bullet bullet;

            if (_poolablesQueue.Count < 1) await Spawn(5, _parent);

            bullet = _poolablesQueue.Dequeue();

            _poolablesDequeued.Add(bullet);

            bullet.transform.position = position;

            bullet.transform.rotation = rotation;

            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public override async void PutIn(Bullet poolable, float delay = 0)
        {
            await AsyncHelper.Delay(delay);

            if(poolable)
            {
                poolable.gameObject.SetActive(false);

                poolable.transform.SetParent(_parent);

                poolable.transform.position = Vector3.zero;

                poolable.transform.rotation = Quaternion.identity;

                _poolablesQueue.Enqueue(poolable);

                _poolablesDequeued.Remove(poolable);
            }
        }

        public override async void PutInAll()
        {
            foreach(var poolable in _poolablesDequeued)
            {
                PutIn(poolable);

                await AsyncHelper.Delay();
            }
        }

    }
}