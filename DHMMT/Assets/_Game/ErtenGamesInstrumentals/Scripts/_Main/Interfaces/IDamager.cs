using Identifiers;

namespace Interfaces
{
    public interface IDamager : IInitializable<IDamagerActor>
    {
        public IDamagerActor damagerActor { get; }
        public IdentifierBase damagerGameobject { get; }
        public void Damage(IDamagable damagable, float damage);
    }
}