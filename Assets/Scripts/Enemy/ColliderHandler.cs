using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ColliderHandler : MonoBehaviour
{
    public event Action<Bullet> Hit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            Hit?.Invoke(bullet);
        }
    }
}
