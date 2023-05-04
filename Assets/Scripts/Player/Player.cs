using System;
using UnityEngine;

[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(PlayerShop))]
[RequireComponent(typeof(PlayerShoot))]
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerShop _playerShop;
    private PlayerWeapon _playerWeapon;
    private PlayerShoot _playerShoot;
    private PlayerMove _playerMove;

    public int Score { get; private set; }
    public int Money { get; private set; }

    public event Action<int> MoneyChanged;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerShop = GetComponent<PlayerShop>();
        _playerWeapon = GetComponent<PlayerWeapon>();
        _playerShoot = GetComponent<PlayerShoot>();
        _playerMove = GetComponent<PlayerMove>();

        _playerInput.Player.Buy.performed += context => OnBuy();
        _playerInput.Player.Reload.performed += context => OnReload();
        _playerInput.Player.Switch.performed += context => SwitchWeapon();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        Money = 0;
        MoneyChanged?.Invoke(Money);
    }

    private void Update()
    {
        _playerShoot.Shoot(_playerInput);
        _playerMove.Move(_playerInput);
    }

    public void AddReward(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void IncreaseScore(int value)
    {
        Score += value;
    }

    public void PayWeapon(WeaponShop weaponShop)
    {
        Money -= weaponShop.Price;
        MoneyChanged?.Invoke(Money);
    }

    public void OnBuy()
    {
        _playerShop.OnBuy(_playerWeapon, this);
    }

    public void SwitchWeapon()
    {
        _playerWeapon.SwitchWeapon();
    }

    public void OnReload()
    {
        _playerWeapon.OnReload();
    }
}
