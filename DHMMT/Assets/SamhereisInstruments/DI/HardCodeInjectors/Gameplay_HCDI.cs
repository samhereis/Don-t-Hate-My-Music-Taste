using ConstStrings;
using DataClasses;
using Events;
using Interfaces;
using Values;

namespace DI
{
    public class Gameplay_HCDI : HardCodeDependencyInjectorBase
    {
        public override void InjectEventsWithParameters()
        {
            DIBox.Add(new EventWithOneParameters<IDamagable>(Event_DIStrings.onEnemyDied), Event_DIStrings.onEnemyDied);
        }

        public override void InjecValueEvents()
        {
            DIBox.Add(new ValueEvent<bool>(Event_DIStrings.isPlayerAiming), Event_DIStrings.isPlayerAiming);

            var playerHealthDataValue = new ValueEvent<PlayerHealthData>(Event_DIStrings.playerHealth);
            playerHealthDataValue.ChangeValue(new PlayerHealthData(100, 100, 100));
            DIBox.Add(playerHealthDataValue, Event_DIStrings.playerHealth);

            DIBox.Add(new ValueEvent<PlayerWeaponData>(Event_DIStrings.playerWeaponData), Event_DIStrings.playerWeaponData);
        }

        public override void ClearEventsWithParameters()
        {
            DIBox.Remove<EventWithOneParameters<IDamagable>>(Event_DIStrings.onEnemyDied);
        }

        public override void ClearValueEvents()
        {
            DIBox.Remove<ValueEvent<bool>>(Event_DIStrings.isPlayerAiming);
            DIBox.Remove<ValueEvent<PlayerHealthData>>(Event_DIStrings.playerHealth);
            DIBox.Remove<ValueEvent<PlayerWeaponData>>(Event_DIStrings.playerWeaponData);
        }
    }
}