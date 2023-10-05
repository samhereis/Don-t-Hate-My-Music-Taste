using Interfaces;

namespace DataClasses
{
    public struct ADamage
    {
        public IDamagerActor damagerActor;
        public IDamagerObject damagerObject;
        public IDamagable damagable;

        public float damageAmount;
        public float healthAfterDamage;
    }
}
