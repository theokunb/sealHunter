using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private string _label;
    [SerializeField] private int _enemyCount;
    [SerializeField] private List<Enemy> _template;
    [SerializeField] private float _maxDelayBetweenEnemy;
    [SerializeField] private float _minDelayBetweenEnemy;
    [SerializeField] private Enemy _boss;

    private int _spawnedEnemyId;
    private int _spawndedCount;
    private bool _bossSpawned;

    public int EnemyCount
    {
        get
        {
            int count = _enemyCount * (int)_template?.Count;
            return _boss == null ? count : count + 1;
        }
    }
    public bool BossCompleated => _boss == null || _boss.IsAlive == false;
    public string Label => _label;

    public bool TryGetBoss(out Enemy boss)
    {
        boss = _boss;

        if (boss == null || _bossSpawned == true)
        {
            return false;
        }
        else
        {
            _bossSpawned = true;
            return true;
        }
    }

    public bool TryGetEnemy(out Enemy enemy)
    {
        enemy = null;

        if (_template.Count == 0)
        {
            return false;
        }

        if (_spawnedEnemyId == _template.Count)
        {
            _spawnedEnemyId = 0;
            _spawndedCount++;
        }

        if (_spawndedCount == _enemyCount)
        {
            return false;
        }

        enemy = _template[_spawnedEnemyId++];
        return true;
    }

    public float GetDelay()
    {
        return Random.Range(_minDelayBetweenEnemy, _maxDelayBetweenEnemy);
    }
}
