using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;

    public void Render(Weapon weapon)
    {
        _icon.sprite = weapon.Sprite;
        _price.text = $"${weapon.Price}";
    }
}
