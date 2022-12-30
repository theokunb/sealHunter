using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<WeaponShop> _weapons;
    [SerializeField] private GameObject _container;
    [SerializeField] private WeaponView _template;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Player _player;

    private void Start()
    {
        foreach (var weapon in _weapons)
        {
            AddItem(weapon);
        }
    }

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void AddItem(WeaponShop weapon)
    {
        var view = Instantiate(_template, _container.transform);
        view.Render(weapon);
    }

    public bool TryBuyWeapon(out WeaponShop weapon, int money)
    {
        weapon = null;

        for (int i = _weapons.Count - 1; i >= 0; i--)
        {
            if (money >= _weapons[i].Price)
            {
                weapon = _weapons[i];
                return true;
            }
        }
        return false;
    }

    private void OnMoneyChanged(int money)
    {
        _money.text = $"Money: {money}";
    }
}
