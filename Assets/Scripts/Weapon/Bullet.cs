using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private int _damage;
    private float _speed;
    private bool _isPiercing;
    private BloodQuality _blood;
    private Rigidbody2D _rigidbody;

    public int Damage => _damage;
    public float Speed => _speed;
    public BloodQuality BloodQuality => _blood;

    protected virtual void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.AddForce(transform.right * -1 * Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<BulletDestroyer>(out _))
        {
            gameObject.SetActive(false);
        }

        if (collision.TryGetComponent<ColliderHandler>(out _) && _isPiercing == false)
        {
            gameObject.SetActive(false);
        }
    }
    public virtual void Initialize(int damage, float speed, bool isPiercing, BloodQuality blood)
    {
        _damage = damage;
        _speed = speed;
        _isPiercing = isPiercing;
        _blood = blood;
    }

    public virtual void SetPosition(Transform shootPoint)
    {
        transform.position = shootPoint.position;
    }
}
