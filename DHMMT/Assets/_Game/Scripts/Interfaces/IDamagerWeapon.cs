namespace Interfaces
{
    public interface IDamagerWeapon : IInitializable<IDamagerActor>
    {
        public IDamagerActor damagerActor { get; }
        public void Damage(IDamagable damagable, float damage);
    }
}