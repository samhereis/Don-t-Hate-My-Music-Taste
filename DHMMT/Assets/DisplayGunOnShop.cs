using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGunOnShop : MonoBehaviour
{
    [SerializeField] Image GunIcon;
    [SerializeField] TMPro.TextMeshProUGUI GunName;
    [SerializeField] TMPro.TextMeshProUGUI GunDamage;
    [SerializeField] TMPro.TextMeshProUGUI GunCost;

    public void SetData(Sprite icon, string name, float damage, int cost)
    {
        GunIcon.sprite = icon;
        GunName.text = name;
        GunDamage.text = damage.ToString();
        GunCost.text = cost.ToString();
    }
}
