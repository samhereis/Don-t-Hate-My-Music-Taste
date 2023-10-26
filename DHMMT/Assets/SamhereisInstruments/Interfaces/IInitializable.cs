using UnityEngine;

namespace Interfaces
{
    public interface IInitializable
    {
        public virtual bool GetCanInitializeWithDI()
        {
            return true;
        }

        public void Initialize();
    }

    public interface IInitializableAsync
    {
        public Awaitable InitializeAsync();
    }

    public interface IInitializable<T>
    {
        public void Initialize(T type);
    }

    public interface IInitializable<T1, T2>
    {
        public void Initialize(T1 param1, T2 param2);
    }
}