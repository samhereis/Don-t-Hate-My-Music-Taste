using DataClasses;
using Identifiers;

namespace Interfaces
{
    public interface IDamagerActor
    {
        public IdentifierBase damagerIdentifier { get; }
        public void OnDamaged(ADamage aDamage);
    }
}