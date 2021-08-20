using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour, IUIPage
{
    public static ShopUI instance;

    public List<ScriptableGun> guns;

    [SerializeField] List<Transform> gunCategories;

    [SerializeField] GameObject GunSlotPrefab;

    bool GunsAreLoaded = false;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] GameObject _page;

    void Awake()
    {
        instance = this;
    }

    public void Enable()
    {
        PauseUnpause.SetPause(true);

        GameplayUI.instance.Disable();

        LoadAllGuns();

        Page.SetActive(true);

        PlayerInput.input.UI.Back.performed += BackStatics.Back;
    }

    public void Disable()
    {
        PlayerInput.input.UI.Back.performed -= BackStatics.Back;

        PauseUnpause.SetPause(false);

        GameplayUI.instance.Enable();

        Page.SetActive(false);
    }

    void LoadAllGuns()
    {
        if(GunsAreLoaded == true)
        {
            return;
        }

        foreach(ScriptableGun gun in guns)
        {
            GameObject gunSlotPrefab = Instantiate(GunSlotPrefab);

            gunSlotPrefab.GetComponent<DisplayGunOnShop>().SetData(gun);

            if(gun.gunType == ScriptableGun.GunTypes.Pistol)
            {
                gunSlotPrefab.transform.SetParent(gunCategories[0]);
            }
            else  if (gun.gunType == ScriptableGun.GunTypes.Rifle)
            {
                gunSlotPrefab.transform.SetParent(gunCategories[1]);
            }

            GunsAreLoaded = true;
        }
    }
}
