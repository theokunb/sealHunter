using System.Collections;
using UnityEngine;

public class Hipster : Humanoid
{
    [SerializeField] private Enemy _sealBaby;
    [SerializeField] private Transform _dropPoint;

    protected override void Die()
    {
        _sealBaby.transform.parent = null;
        StartCoroutine(DropSealBabyTask());
        base.Die();
    }

    private IEnumerator DropSealBabyTask()
    {
        while (_sealBaby.transform.position != _dropPoint.position)
        {
            _sealBaby.transform.position = Vector3.MoveTowards(_sealBaby.transform.position, _dropPoint.position, Time.deltaTime);
            yield return null;
        }

        _sealBaby.SetTarget(Target);

        if (_sealBaby.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            if (_sealBaby.IsAlive == false)
            {
                spriteRenderer.sortingOrder = 0;
            }
        }
    }
}
