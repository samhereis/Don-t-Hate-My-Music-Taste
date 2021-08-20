using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGunOnShop : MonoBehaviour
{
    [SerializeField] ScriptableGun gun;

    [SerializeField] Image GunIcon;
    [SerializeField] TMPro.TextMeshProUGUI GunName;
    [SerializeField] TMPro.TextMeshProUGUI GunDamage;
    [SerializeField] TMPro.TextMeshProUGUI GunCost;

    void Awake()
    {
        
    }

    public void SetData(ScriptableGun Sentgun)
    {
        gun = Sentgun;

        GunIcon.sprite = gun.gunIcon;
        GunName.text = gun.gunName;
        GunDamage.text = gun.gunDamage.ToString();
        GunCost.text = gun.gunCost.ToString();
    }

    public void TryBuy()
    {
        if (PlayerKillCount.instance.GetKillCount() >= gun.gunCost)
        {
            GameObject newGun = Instantiate(gun.gunPrefab.gameObject);

            PlayerGunUse.instance.SecodWeapon = newGun.GetComponent<InteractableEquipWeapon>();

            PlayerGunUse.instance.SecodWeapon.Interact(PlayerGunUse.instance.gameObject);

            PlayerGunUse.instance.DefaultWeapon.gameObject.SetActive(false);
        }
        else
        {
            transform.DOShakePosition(2f, 10f, 10, 50, true, true);
        }
    }
}
