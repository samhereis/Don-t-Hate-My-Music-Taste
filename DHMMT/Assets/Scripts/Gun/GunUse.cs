using Characters.States.Data;
using Helpers;
using Pooling;
using Sound;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

namespace Gameplay
{
    public class GunUse : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InteractableEquipWeapon _interactableEquipWeapon;
        [SerializeField] private Animator _weaponAnimator;

        [Header("Sounds")]
        [SerializeField] private SimpleSoundPlayer _shootSound;

        [Header("Ammo")]
        [SerializeField] private int _currentAmmo = 8;
        [SerializeField] private int _maxAmmo = 50;

        [Header("Bullet")]
        [SerializeField] private BulletPooling_SO _bullet;
        [SerializeField] private string _bulletPoolerKey;
        [SerializeField] private Transform _bulletPosition;

        [Header("Shoot Timing")]
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private float _nextFire;
        [SerializeField] private float _reloadTime;

        [Header("Shoot States")]
        [SerializeField] private bool _isReloading = false;
        [SerializeField] private bool _canShoot = true;
        [SerializeField] private bool _shoot = false;

        private async void Awake()
        {
            _weaponAnimator ??= GetComponentInChildren<Animator>();

            _interactableEquipWeapon?.onEquip.AddListener(OnEquip);
            _interactableEquipWeapon?.onUnequip.AddListener(OnUnEquip);

            if (_bullet == null) _bullet = await AddressablesHelper.GetAssetAsync<BulletPooling_SO>(_bulletPoolerKey);
        }

        private void OnDestroy()
        {
            _interactableEquipWeapon?.onEquip.RemoveListener(OnEquip);
            _interactableEquipWeapon?.onUnequip.RemoveListener(OnUnEquip);
        }

        private void FixedUpdate()
        {
            if (_shoot) TryShoot();
        }

        private void OnEquip(HumanoidData sentData)
        {
            sentData.humanoidAttackingStateData.onAttack += SetShoot;
        }

        private void OnUnEquip(HumanoidData sentData)
        {
            sentData.humanoidAttackingStateData.onAttack -= SetShoot;
        }

        public void SetShoot(bool value)
        {
            _shoot = value;
        }

        private async void TryShoot()
        {
            if ((Time.time > _nextFire) && (_canShoot == true))
            {
                _nextFire = Time.time + _fireRate;

                var bullet = await _bullet.PutOffAsync(_bulletPosition, _bulletPosition.rotation);

                _shootSound.Play();

                _weaponAnimator.SetTrigger("Shoot");

                DecreaseAmmo();
            }
        }

        public void DecreaseAmmo(int value = 1)
        {
            _currentAmmo -= value;
            if (_currentAmmo <= 0)
            {
                _canShoot = false;
                Reload();
            }
        }

        public void IncreaseAmmo(int value = 1)
        {
            _currentAmmo += value;
        }

        public void IncreaseAmmoAmmoInPersentageRelativeToMaxAmmo(int value)
        {
            _currentAmmo = _maxAmmo * value;
            if (_currentAmmo > _maxAmmo) _currentAmmo = _maxAmmo;
        }

        public async void Reload()
        {
            while (_currentAmmo < _maxAmmo)
            {
                await AsyncHelper.Delay();
                IncreaseAmmo();
            }

            if (_currentAmmo >= _maxAmmo) _canShoot = true;
        }
    }
}