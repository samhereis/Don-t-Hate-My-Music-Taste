using ConstStrings;
using DataClasses;
using Identifiers;
using Interfaces;
using Observables;

namespace DependencyInjection
{
    public class EFH_DependencyInstaller : DependencyInstallerBase
    {
        private ObservableValue<bool> _isPlayerAiming = new(ObservableValue_ConstStrings.isPlayerAiming);
        private DataSignal<IDamagable> _onEnemyDied = new(DataSignal_ConstStrings.onEnemyDied);
        private DataSignal<Exit_Identifier> _onExitFound = new(DataSignal_ConstStrings.onExitFound);
        private DataSignal<GameplayStatus> _onGameplayStatusChanged = new(DataSignal_ConstStrings.onGameplayStatusChaned);
        private ObservableValue<PlayerWeaponData> _playerWeaponData = new(ObservableValue_ConstStrings.playerWeaponData);
        private ObservableValue<PlayerHealthData> _playerHealthValue = new(ObservableValue_ConstStrings.playerHealth);

        public override void Inject()
        {
            base.Inject();
            InjectSignals();
        }

        public override void Clear()
        {
            base.Clear();
            InjectSignals();
        }

        private void InjectSignals()
        {
            _playerHealthValue.value = new PlayerHealthData(100, 100, 100);

            DependencyContext.diBox.Add<ObservableValue<bool>>(_isPlayerAiming, ObservableValue_ConstStrings.isPlayerAiming);
            DependencyContext.diBox.Add<DataSignal<IDamagable>>(_onEnemyDied, DataSignal_ConstStrings.onEnemyDied);
            DependencyContext.diBox.Add<DataSignal<Exit_Identifier>>(_onExitFound, DataSignal_ConstStrings.onExitFound);
            DependencyContext.diBox.Add<DataSignal<GameplayStatus>>(_onGameplayStatusChanged, DataSignal_ConstStrings.onGameplayStatusChaned);
            DependencyContext.diBox.Add<ObservableValue<PlayerWeaponData>>(_playerWeaponData, ObservableValue_ConstStrings.playerWeaponData);
            DependencyContext.diBox.Add<ObservableValue<PlayerHealthData>>(_playerHealthValue, ObservableValue_ConstStrings.playerHealth);
        }

        private void ClearSignals()
        {
            DependencyContext.diBox.Remove<ObservableValue<bool>>(ObservableValue_ConstStrings.isPlayerAiming);
            DependencyContext.diBox.Remove<DataSignal<IDamagable>>(DataSignal_ConstStrings.onEnemyDied);
            DependencyContext.diBox.Remove<DataSignal<Exit_Identifier>>(DataSignal_ConstStrings.onExitFound);
            DependencyContext.diBox.Remove<DataSignal<GameplayStatus>>(DataSignal_ConstStrings.onGameplayStatusChaned);
            DependencyContext.diBox.Remove<ObservableValue<PlayerWeaponData>>(ObservableValue_ConstStrings.playerWeaponData);
            DependencyContext.diBox.Remove<ObservableValue<PlayerHealthData>>(ObservableValue_ConstStrings.playerHealth);
        }
    }
}