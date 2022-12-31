using System.Collections;
using UnityEngine;

public class PolarBear : Enemy
{
    private const float SlowFactor = 0.25f;

    [SerializeField] private float _acceleration;

    protected override void Update()
    {
        base.Update();
    }

    protected override void TakeDamage(int damage, bool headHitted)
    {
        base.TakeDamage(damage, headHitted);
        Speed += _acceleration;
        Animator.speed += _acceleration;
    }

    protected override void Die()
    {
        Animator.SetInteger(EnemyAnimatorController.Params.Health, CurrentHealth);
        StartCoroutine(Slide());
    }

    private IEnumerator Slide()
    {
        while (Speed > 0)
        {
            Speed -= SlowFactor * Time.deltaTime;
            yield return null;
        }

        base.Die();
    }
}
