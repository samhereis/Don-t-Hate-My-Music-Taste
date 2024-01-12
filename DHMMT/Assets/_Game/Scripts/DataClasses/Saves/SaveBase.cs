using System;

namespace DataClasses
{
    [Serializable]
    public abstract class SaveBase : ISavable
    {
        public abstract string folderName { get; }
        public abstract string fileName { get; }

        public abstract SaveBase CreateInstance<T>();
    }
}