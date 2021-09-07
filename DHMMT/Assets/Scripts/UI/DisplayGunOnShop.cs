using DG.Tweening;
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
        GunCost.text = $"ㄙ {gun.gunCost}";
    }

    public void TryBuy()
    {
        transform.DOShakePosition(2f, 10f, 10, 50, false, true);

        if (PlayerKillCount.instance.GetKillCount() >= gun.gunCost)
        {
            GameObject newGun = Instantiate(gun.gunPrefab.gameObject);

            if (gun.gunType == ScriptableGun.GunTypes.Pistol)
            {
                if (PlayerGunUse.instance.DefaultWeapon != null)
                {
                    PlayerGunUse.instance.DefaultWeapon.Remove();
                }

                PlayerGunUse.instance.DefaultWeapon = newGun.GetComponent<InteractableEquipWeapon>();

                PlayerGunUse.instance.DefaultWeapon.Interact(PlayerGunUse.instance.gameObject);

                PlayerGunUse.instance.ChangeWeapon(ScriptableGun.GunTypes.Pistol);
            }
            else if (gun.gunType == ScriptableGun.GunTypes.Rifle)
            {
                if (PlayerGunUse.instance.SecodWeapon != null)
                {
                    PlayerGunUse.instance.SecodWeapon.Remove();
                }

                PlayerGunUse.instance.SecodWeapon = newGun.GetComponent<InteractableEquipWeapon>();

                PlayerGunUse.instance.SecodWeapon.Interact(PlayerGunUse.instance.gameObject);

                PlayerGunUse.instance.ChangeWeapon(ScriptableGun.GunTypes.Rifle);
            }
        }
        else
        {
            AnimationStatics.NormalShake(transform, 2);

            MessageScript.instance.ShowMessage(MessageScript.instance.DontHaveEnoughKills, 5);
        }
    }
}
