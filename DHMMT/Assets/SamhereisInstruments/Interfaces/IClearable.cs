using UnityEngine;

namespace Interfaces
{
    public interface IClearable
    {
        public void Clear();
    }

    public interface IClearableAsync
    {
        public Awaitable ClearAsync();
    }

    public interface IClearable<T>
    {
        public void Clear(T type);
    }
}