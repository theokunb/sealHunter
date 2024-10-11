using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Humanoid : MonoBehaviour
{
    private const float DamageForHeadShot = 1.5f;

    [SerializeField] private ColliderHandler _head;
    [SerializeField] private ColliderHandler _body;
    [SerializeField] private float _speed;
    [SerializeField] private int _baseHealth;
    [SerializeField] private float _bonusHealthForLevel;
    [SerializeField] private int _reward;
    [SerializeField] private int _rewardForHeadShot;
    [SerializeField] private Blood _blood;

    private int _currentHealth;
    private int _maxHealth;
    private AudioSource _sound;

    protected Animator Animator { get; private set; }
    protected Transform Target { get; private set; }
    protected float Speed;
    protected int CurrentHealth => _currentHealth;

    public bool IsAlive => _currentHealth > 0;
    public AudioSource Sound => _sound;

    public event Action<int> PayReward;
    public event Action<int, int> HealthChanged;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Animator.SetInteger(EnemyAnimatorController.Params.Health, _baseHealth);
        Speed = _speed;
        _sound = GetComponent<AudioSource>();
    }

    protected virtual void OnEnable()
    {
        _head.Hit += OnHeadHitted;
        _body.Hit += OnBodyHitted;
    }

    protected virtual void OnDisable()
    {
        _head.Hit -= OnHeadHitted;
        _body.Hit -= OnBodyHitted;
    }

    protected virtual void Update()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }

    protected virtual void TakeDamage(int damage, bool headHitted)
    {
        damage = headHitted ? (int)(damage * DamageForHeadShot) : damage;

        _currentHealth -= damage;
        HealthChanged?.Invoke(CurrentHealth, _maxHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        TriggerAnimationOnDie(_currentHealth);
        PayReward?.Invoke(_reward);
        
        _head.gameObject.SetActive(false);
        _body.gameObject.SetActive(false);
        enabled = false;

        var spawnObject = GameObject.Find("Spawner");

        if (spawnObject.TryGetComponent(out Spawner spawner))
        {
            spawner.OnEnemyDied(this);
        }
    }

    public void Initialize(int level)
    {
        _maxHealth = (int)(_baseHealth + _bonusHealthForLevel * level);
        _currentHealth = _maxHealth;
        Animator.SetInteger(EnemyAnimatorController.Params.Health, _currentHealth);
    }

    public virtual void SetTarget(Transform target)
    {
        Animator.SetTrigger(EnemyAnimatorController.Params.Walk);
        Target = target;
    }

    private void OnHeadHitted(Bullet bullet)
    {
        TakeDamage(bullet.Damage, true);
        CreateBlood(bullet.BloodQuality);
        PayReward?.Invoke(_rewardForHeadShot);
    }

    private void OnBodyHitted(Bullet bullet)
    {
        TakeDamage(bullet.Damage, false);
        CreateBlood(bullet.BloodQuality);
    }

    private void CreateBlood(BloodQuality bloodQuality)
    {
        var blood = Instantiate(_blood, transform.position, Quaternion.identity);
        blood.Trigger(bloodQuality);
    }

    private void TriggerAnimationOnDie(int health)
    {
        Animator.SetInteger(EnemyAnimatorController.Params.Health, health);
    }
}
