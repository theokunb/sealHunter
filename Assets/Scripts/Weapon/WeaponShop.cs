using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _sprite;

    public Sprite Sprite => _sprite;
    public int Price => _price;
    public Weapon Weapon => _weapon;
}
