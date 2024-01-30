using Interfaces;

namespace DataClasses
{
    public struct PostDamageInfo
    {
        public IDamagerWeapon damagerObject;
        public IDamagable damagable;

        public float damageAmount;
        public float healthAfterDamage;
    }
}
