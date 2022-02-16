using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUI : MonoBehaviour
{
    // Manage gun shop

    public static ShopUI instance;

    public List<ScriptableGun> Guns;

    [SerializeField] private Transform _pistolCategoriyPage;
    [SerializeField] private Transform _rifleCategoriyPage;

    [SerializeField] private GameObject _gunSlotPrefab;

    private bool _gunsAreLoaded = false;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] private GameObject _page;

    public void Enable()
    {
        LoadAllGuns();

        Page.SetActive(true);
    }

    public void Disable()
    {
        Page.SetActive(false);
    }

    private void LoadAllGuns()
    {
        if(_gunsAreLoaded == true)
        {
            return;
        }

        foreach(ScriptableGun gun in Guns)
        {
            GameObject gunSlotPrefab = Instantiate(_gunSlotPrefab);

            gunSlotPrefab.GetComponent<DisplayGunOnShop>().SetData(gun);

            if(gun.gunType == ScriptableGun.GunTypes.Pistol)
            {
                gunSlotPrefab.transform.SetParent(_pistolCategoriyPage);
            }
            else  if (gun.gunType == ScriptableGun.GunTypes.Rifle)
            {
                gunSlotPrefab.transform.SetParent(_rifleCategoriyPage);
            }

            _gunsAreLoaded = true;
        }
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = this;
    }
}
