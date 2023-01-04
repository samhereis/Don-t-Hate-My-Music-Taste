using Data;
using System.Collections;
using System.Collections.Generic;
using UI.Displayers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class ShopUI : MonoBehaviour
    {
        [field: SerializeField] public List<ScriptableGun> guns { get; private set; }

        [SerializeField] private Transform _pistolCategoriyPage;
        [SerializeField] private Transform _rifleCategoriyPage;

        [SerializeField] private GameObject _gunSlotPrefab;
        [field: SerializeField] public GameObject page { get; private set; }

        private bool _gunsAreLoaded = false;

        public void Enable()
        {
            LoadAllGuns();

            page.SetActive(true);
        }

        public void Disable()
        {
            page.SetActive(false);
        }

        private void LoadAllGuns()
        {
            if (_gunsAreLoaded == true) return;

            foreach (ScriptableGun gun in guns)
            {
                GameObject gunSlotPrefab = Instantiate(_gunSlotPrefab);

                gunSlotPrefab.GetComponent<GunOnShopDisplayer>().SetData(gun);

                if (gun.gunType == ScriptableGun.GunTypes.Pistol)
                {
                    gunSlotPrefab.transform.SetParent(_pistolCategoriyPage);
                }
                else if (gun.gunType == ScriptableGun.GunTypes.Rifle)
                {
                    gunSlotPrefab.transform.SetParent(_rifleCategoriyPage);
                }

                _gunsAreLoaded = true;
            }
        }
    }
}