using System;

namespace DataClasses
{
    [Serializable]
    public abstract class ModeSaveBase : SaveBase
    {
        public override string folderName => "Modes";
    }
}