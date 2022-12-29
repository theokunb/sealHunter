using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShotgunShell : Bullet
{
    private List<Bullet> _bullets;

    private void Awake()
    {
        _bullets = GetComponentsInChildren<Bullet>(true).ToList();
        _bullets.RemoveAt(0);
    }

    private void OnDisable()
    {
        foreach(var bullet in _bullets)
        {
            bullet.transform.position = Vector3.zero;
            bullet.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach(var bullet in _bullets)
        {
            bullet.gameObject.SetActive(true);
        }
    }

    public override void Initialize(int damage, float speed, bool isPiercing, BloodQuality blood)
    {
        foreach(var bullet in _bullets)
        {
            bullet.Initialize(damage, speed, isPiercing, blood);
        }
    }

    public override void SetPosition(Transform transform)
    {
        foreach(var bullet in _bullets)
        {
            bullet.SetPosition(transform);
        }
    }
}
