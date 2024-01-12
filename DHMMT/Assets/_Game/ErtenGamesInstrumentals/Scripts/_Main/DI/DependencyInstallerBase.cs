using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DependencyInjection
{
    public abstract class DependencyInstallerBase : MonoBehaviour
    {
        [ShowInInspector] protected Dictionary<Type, string> _typesToDeleteOnClear = new();

        public virtual void AddWithAutoDelete<T>(T obj, string id = "")
        {
            _typesToDeleteOnClear.Add(typeof(T), id);

            DependencyContext.diBox.Add<T>(obj, id);
        }

        public virtual void Inject()
        {

        }

        public virtual void Clear()
        {
            foreach (var type in _typesToDeleteOnClear)
            {
                DependencyContext.diBox.Remove(type.Key, type.Value);
            }
        }
    }
}