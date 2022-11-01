using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LazyUpdators
{
    [CreateAssetMenu(fileName = "LazyUpdator", menuName = "Scriptables/LazyUpdator")]
    public class LazyUpdator_SO : ScriptableObject
    {
        private List<Func<Task>> _tasks = new List<Func<Task>>();

        public void AddToQueue(Func<Task> task)
        {
            if (_tasks.Count == 0) DoLazyUpdateWithAwait(Add); else Add();

            void Add() { _tasks.SafeAdd(task); }
        }

        public void RemoveFromQueue(Func<Task> task)
        {
            _tasks.SafeRemove(task);
        }

        private async void DoLazyUpdateWithAwait(Action doBeforeUpdate = null)
        {
            doBeforeUpdate?.Invoke();

            while (_tasks.Count > 0)
            {
                await Task.Run(() =>
                {
                    Parallel.ForEach(_tasks.ToArray(), async (action) =>
                    {
                        await action?.Invoke();
                    });
                });
            }
        }
    }
}