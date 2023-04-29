using System;
using UnityEngine;

[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShoot : MonoBehaviour
{
    private Rigidbody2D _rigibody;
    private PlayerWeapon _playerWeapon;

    public event Action<AudioSource> PlayerShooted;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        _playerWeapon = GetComponent<PlayerWeapon>();
    }

    public void Shoot(PlayerInput playerInput)
    {
        var shootValue = playerInput.Player.Shoot.ReadValue<float>();

        Shoot(shootValue);
    }

    public void Shoot(float value)
    {
        if(value == 0)
        {
            return;
        }

        if (_playerWeapon.CurrentWeapon.TryShoot() == true)
        {
            _rigibody.AddForce(Vector2.right * _playerWeapon.CurrentWeapon.Recoil);
            PlayerShooted?.Invoke(_playerWeapon.CurrentWeapon.Sound);
        }
    }
}
