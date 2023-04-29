using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    [SerializeField] private int _damageToHide;
    [SerializeField] private float _hideTime;

    private float _elapsedTime = 0;
    private float _takenDamage = 0;
    private bool _isHided = false;

    protected override void Update()
    {
        if(_isHided == false)
        {
            base.Update();
        }
    }

    protected override void TakeDamage(int damage, bool headHitted)
    {
        if(_isHided == true)
        {
            return;
        }

        base.TakeDamage(damage, headHitted);
        _takenDamage += damage;

        if(_takenDamage >= _damageToHide)
        {
            Hide();
        }
    }

    private void Hide()
    {
        Animator.SetTrigger(TurtleAnimationController.Params.Hide);
        StartCoroutine(HideTask());
    }

    private IEnumerator HideTask()
    {
        _isHided = true;
        _elapsedTime = 0;

        while(_elapsedTime <= _hideTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        Animator.SetTrigger(TurtleAnimationController.Params.Open);
        _isHided = false;
        _takenDamage = 0;
    }
}
