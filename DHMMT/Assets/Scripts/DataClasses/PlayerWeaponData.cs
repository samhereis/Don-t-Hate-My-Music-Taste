using Gameplay.Bullets;
using Identifiers;

namespace DataClasses
{
    public struct PlayerWeaponData
    {
        public int maxAmmo;
        public int currentAmmo;

        public WeaponIdentifier weapon;

        public PlayerWeaponData(int maxAmmo, int currentAmmo, WeaponIdentifier weapon)
        {
            this.maxAmmo = maxAmmo;
            this.currentAmmo = currentAmmo;
            this.weapon = weapon;
        }
    }
}