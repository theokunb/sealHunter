using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _leftHandPlace;
    [SerializeField] private Transform _rightHandPlace;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _clip;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private int _damage;
    [SerializeField] private int _clipSize;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private bool _isPiercing;
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _recoil;
    [SerializeField] private float _delayBetweenShoot;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private BloodQuality _blood;

    private Stack<Bullet> _bullets;
    private Stack<Bullet> _wastedBullets;
    private float _elapsedReloadTime;
    private float _shootTime;
    private AudioSource _sound;

    public Sprite Sprite => _sprite;
    public bool IsReloading { get; private set; }
    public Transform LeftHandPlace => _leftHandPlace;
    public Transform RightHandPlace => _rightHandPlace;
    public Transform ShootPoint => _shootPoint;
    public int Price => _price;
    public float Recoil => _recoil;
    public AudioSource Sound => _sound;

    public event Action<int> BulletsInClicpChanged;

    private void OnEnable()
    {
        BulletsInClicpChanged?.Invoke(_bullets.Count);
    }

    private void Start()
    {
        _bullets = new Stack<Bullet>();
        _wastedBullets= new Stack<Bullet>();
        _sound = GetComponent<AudioSource>();

        for (int i = 0; i < _clipSize; i++)
        {
            var bullet = Instantiate(_bulletTemplate, _clip);
            bullet.gameObject.SetActive(false);
            bullet.Initialize(_damage, _bulletSpeed, _isPiercing, _blood);
            _bullets.Push(bullet);
        }

        BulletsInClicpChanged?.Invoke(_bullets.Count);
    }

    private void Update()
    {
        _shootTime += Time.deltaTime;
    }

    public bool TryShoot()
    {
        if (_bullets.Count == 0 || IsReloading == true || _shootTime < _delayBetweenShoot)
        {
            return false;
        }

        _shootTime = 0;
        var bullet = _bullets.Pop();
        bullet.transform.SetParent(null);
        bullet.gameObject.SetActive(true);
        bullet.SetPosition(_shootPoint);
        _wastedBullets.Push(bullet);
        BulletsInClicpChanged?.Invoke(_bullets.Count);

        return true;
    }

    public void Reload()
    {
        if (IsReloading == true || _bullets.Count == _clipSize)
        {
            return;
        }
        else
        {
            StartCoroutine(OnReaload());
            IsReloading = true;
        }
    }

    private IEnumerator OnReaload()
    {
        while(_elapsedReloadTime <= _reloadTime)
        {
            _elapsedReloadTime += Time.deltaTime;
            if (_wastedBullets.Count > 0)
            {
                var bullet = _wastedBullets.Pop();
                _bullets.Push(bullet);
                bullet.gameObject.SetActive(false);
                bullet.transform.SetParent(_clip);
            }

            yield return null;
        }

        _elapsedReloadTime = 0;
        IsReloading = false;
        BulletsInClicpChanged?.Invoke(_bullets.Count);
    }
}
