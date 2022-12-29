using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private Transform _weaponPlace;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Shop _shop;
    [SerializeField] private TMP_Text _clip;

    private const float SlideFactor = 0.993f;

    private Animator _animator;
    private PlayerInput _playerInput;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigibody;
    private Weapon _currentWeapon;
    private Weapon _secondWeapon;
    private int _money = 0;
    private int _score = 0;

    public Weapon CurrentWeapon => _currentWeapon;
    public int Score => _score;

    public event Action<int> MoneyChanged;
    public event Action<AudioSource> PlayerShooted;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _rigibody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _playerInput.Player.Reload.performed += context => OnReload();
        _playerInput.Player.Buy.performed += context => OnBuy();
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
        MoneyChanged?.Invoke(_money);
        _currentWeapon = Instantiate(_defaultWeapon, _weaponPlace);
        Equip(_currentWeapon);
    }

    private void Update()
    {
        _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
        var shoot = _playerInput.Player.Shoot.ReadValue<float>();

        if (shoot > 0)
        {
            OnShoot();
        }

        Move(_moveDirection);
    }

    private void OnShoot()
    {
        if (_currentWeapon.TryShoot() == true)
        {
            _rigibody.AddForce(Vector2.right * _currentWeapon.Recoil);
            PlayerShooted?.Invoke(_currentWeapon.Sound);
        }
    }

    private void OnReload()
    {
        _currentWeapon.Reload();
    }

    private void OnBuy()
    {
        if (_currentWeapon.IsReloading == true)
        {
            return;
        }

        if (_shop.TryBuyWeapon(out Weapon weapon, _money))
        {
            _money -= weapon.Price;
            MoneyChanged?.Invoke(_money);

            if (_secondWeapon != null)
            {
                Destroy(_secondWeapon.gameObject);
            }

            _secondWeapon = Instantiate(weapon, _weaponPlace);
            SwitchWeapon();
        }
    }

    private void Move(Vector2 direction)
    {
        _animator.SetFloat(PlayerAnimationController.Params.Speed, direction.sqrMagnitude);

        if (direction.sqrMagnitude < PlayerAnimationController.Params.WalkEps)
        {
            _rigibody.velocity *= SlideFactor;
            return;
        }

        _rigibody.velocity += direction * _speed * Time.deltaTime;
    }

    private void Equip(Weapon weapon)
    {
        _currentWeapon.BulletsInClicpChanged += OnBulletsCountChanged;
        _leftHand.position = _currentWeapon.LeftHandPlace.position;
        _rightHand.position = _currentWeapon.RightHandPlace.position;
        _currentWeapon.gameObject.SetActive(true);
    }

    private void SwitchWeapon()
    {
        if (_secondWeapon == null || _currentWeapon.IsReloading)
        {
            return;
        }

        _currentWeapon.BulletsInClicpChanged -= OnBulletsCountChanged;
        _currentWeapon.gameObject.SetActive(false);
        (_currentWeapon, _secondWeapon) = (_secondWeapon, _currentWeapon);
        Equip(_currentWeapon);
    }

    private void OnBulletsCountChanged(int bullets)
    {
        _clip.text = bullets.ToString();
    }

    public void AddReward(int reward)
    {
        _money += reward;
        MoneyChanged?.Invoke(_money);
    }

    public void IncreaseScore()
    {
        _score++;
    }
}
