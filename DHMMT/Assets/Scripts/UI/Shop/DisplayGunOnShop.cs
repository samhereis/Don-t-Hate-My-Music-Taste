using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

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

        if (true)
        {
            GameObject newGun = Instantiate(_gun.GunPrefab.gameObject);
        }
        else
        {
            AnimationHelper.NormalShake(transform, 2);
        }
    }
}
