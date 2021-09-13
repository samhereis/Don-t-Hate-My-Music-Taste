using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGunOnShop : MonoBehaviour
{
    // On shop display a gun to buy

    [SerializeField] private ScriptableGun _gun;
      
    [SerializeField] private Image _gunIcon;
    [SerializeField] private TMPro.TextMeshProUGUI _gunName;
    [SerializeField] private TMPro.TextMeshProUGUI _gunDamage;
    [SerializeField] private TMPro.TextMeshProUGUI _gunCost;

    public void SetData(ScriptableGun Sentgun)
    {
        _gun = Sentgun;

        _gunIcon.sprite = Sentgun.GunIcon;
        _gunName.text = Sentgun.GunName;
        _gunDamage.text = Sentgun.GunDamage.ToString();
        _gunCost.text = $"ㄙ {Sentgun.GunCost}";
    }

    public void TryBuy()
    {
        transform.DOShakePosition(2f, 10f, 10, 50, false, true);

        if (PlayerKillCount.instance.GetKillCount() >= _gun.GunCost)
        {
            GameObject newGun = Instantiate(_gun.GunPrefab.gameObject);

            if (_gun.GunType == ScriptableGun.GunTypes.Pistol)
            {
                if (PlayerGunUse.instance.DefaultWeapon != null)
                {
                    PlayerGunUse.instance.DefaultWeapon.Remove();
                }

                PlayerGunUse.instance.DefaultWeapon = newGun.GetComponent<InteractableEquipWeapon>();

                PlayerGunUse.instance.DefaultWeapon.Interact(PlayerGunUse.instance.gameObject);

                PlayerGunUse.instance.ChangeWeapon(ScriptableGun.GunTypes.Pistol);
            }
            else if (_gun.GunType == ScriptableGun.GunTypes.Rifle)
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
