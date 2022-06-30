using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Helpers;
using Data;
using Samhereis.Helpers;

namespace UI.Displayers
{
    public class GunOnShopDisplayer : MonoBehaviour
    {
        [SerializeField] private ScriptableGun _gun;

        [SerializeField] private Image _gunIcon;
        [SerializeField] private TMPro.TextMeshProUGUI _gunName;
        [SerializeField] private TMPro.TextMeshProUGUI _gunDamage;
        [SerializeField] private TMPro.TextMeshProUGUI _gunCost;

        public void SetData(ScriptableGun Sentgun)
        {
            _gun = Sentgun;

            _gunIcon.sprite = _gun.gunIcon;
            _gunName.text = _gun.gunName;
            _gunDamage.text = _gun.gunDamage.ToString();
            _gunCost.text = $"ㄙ {_gun.gunCost}";
        }

        public void TryBuy() //TODO: rewrite this
        {
            transform.DOShakePosition(2f, 10f, 10, 50, false, true);

            if (true)
            {
                GameObject newGun = Instantiate(_gun.gunPrefab.gameObject);
            }
            else
            {
                transform.NormalShake(2);
            }
        }
    }
}