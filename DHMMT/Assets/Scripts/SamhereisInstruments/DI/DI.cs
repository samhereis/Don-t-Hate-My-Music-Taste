using System;

namespace DI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class DI : Attribute
    {
        public string Id { get; }

        public DI(string id = "")
        {
            Id = id;
        }
    }
}