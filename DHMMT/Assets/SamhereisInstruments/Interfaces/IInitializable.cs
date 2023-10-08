using UnityEngine;

namespace Interfaces
{
    public interface IInitializable
    {
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
}