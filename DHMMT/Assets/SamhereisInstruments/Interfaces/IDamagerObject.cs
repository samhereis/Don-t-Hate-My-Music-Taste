using Identifiers;

namespace Interfaces
{
    public interface IDamagerObject : IInitializable<IDamagerActor>
    {
        public IDamagerActor damagerActor { get; }
        public IdentifierBase damagerObjectIdentifier { get; }
        public void Damage(IDamagable damagable, float damage);
    }
}