using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : PointCollection
{
    private const string VictoryMessage = "Victory";

    [SerializeField] private Player _player;
    [SerializeField] private PointCollection _target;
    [SerializeField] private Transform _bossPosition;
    [SerializeField] private List<Wave> _waves;

    private int _died = 0;
    private bool _bossSpawned = false;
    private int _level = 0;
    private float _currentProgress = 0;

    public event Action<AudioSource> EnemySpawnedSound;
    public event Action<float> ProgressChanged;
    public event Action<string> LevelChanged;
    public event Action<string> GameWin;

    private void Start()
    {
        Points = GetComponentsInChildren<Transform>();
        ProgressChanged?.Invoke(0);

        if (_waves.Count > 0)
        {
            LevelChanged?.Invoke(_waves.First().Label);
        }

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (_level < _waves.Count)
        {
            yield return new WaitForSeconds(_waves[_level].GetDelay());

            CreateBoss();
            CreateEnemy();
        }

        GameWin?.Invoke(VictoryMessage);
    }

    private void CreateEnemy()
    {
        if(_level >= _waves.Count)
        {
            return;
        }

        if (_waves[_level].TryGetEnemy(out Enemy enemy))
        {
            InstatniateEnemy(enemy, GetRandomPosition());

            if (_bossSpawned == false)
            {
                _currentProgress += 1f / _waves[_level].EnemyCount;
                ProgressChanged?.Invoke(_currentProgress);
            }
        }
    }

    private void CreateBoss()
    {
        if (_level >= _waves.Count)
        {
            return;
        }

        if (_waves[_level].TryGetBoss(out Enemy boss))
        {
            boss = InstatniateEnemy(boss, _bossPosition);
            _bossSpawned = true;
            boss.HealthChanged += OnEnemyHealthChanged;
        }
    }

    private Enemy InstatniateEnemy(Enemy enemy, Transform parent)
    {
        enemy = Instantiate(enemy, parent);
        EnemySpawnedSound?.Invoke(enemy.Sound);
        enemy.Initialize(_level);
        enemy.SetTarget(_target.GetRandomPosition());
        enemy.Died += OnEnemyDied;
        enemy.PayReward += OnPayReward;
        return enemy;
    }

    private void OnEnemyHealthChanged(int currentHealth, int maxHealth)
    {
        _currentProgress = (float)currentHealth / maxHealth;
        ProgressChanged?.Invoke(_currentProgress);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.PayReward -= OnPayReward;
        enemy.Died -= OnEnemyDied;
        enemy.HealthChanged -= OnEnemyHealthChanged;

        _died++;
        _player.IncreaseScore();

        if (_died == _waves[_level].EnemyCount)
        {
            UpgradeLevel();
        }
    }

    private void UpgradeLevel()
    {
        _level++;
        _died = 0;
        _currentProgress = 0;
        _bossSpawned = false;
        ProgressChanged?.Invoke(_currentProgress);

        if (_level < _waves.Count)
        {
            LevelChanged?.Invoke(_waves[_level].Label);
        }
    }

    private void OnPayReward(int reward)
    {
        _player.AddReward(reward);
    }
}