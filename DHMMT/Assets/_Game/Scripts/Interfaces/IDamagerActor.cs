using DataClasses;
using Identifiers;

namespace Interfaces
{
    public interface IDamagerActor
    {
        public IdentifierBase damagerIdentifier { get; }
        public void OnHasDamaged(PostDamageInfo aDamage);
    }
}