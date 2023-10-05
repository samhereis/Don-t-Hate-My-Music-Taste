using Interfaces;

namespace DataClasses
{
    public struct DamageData
    {
        public float damage;

        public IDamagerObject damager;
        public IDamagable damagable;

        public DamageData(float damage, IDamagerObject damager, IDamagable damagable)
        {
            this.damage = damage;
            this.damager = damager;
            this.damagable = damagable;
        }
    }
}