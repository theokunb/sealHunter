using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Enemy
{
    [SerializeField] private float _minTimeForSlide;
    [SerializeField] private float _maxTimeForSlide;
    [SerializeField] private float _slideSpeed;

    private float _elapsedTime;
    private float _timeForSlide;
    private bool _isSliding;

    private void Start()
    {
        _timeForSlide = Random.Range(_minTimeForSlide, _maxTimeForSlide);
    }

    protected override void Update()
    {
        base.Update();

        if(_isSliding == false)
        {
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= _timeForSlide)
            {
                _isSliding = true;
                Animator.SetTrigger(PenguinAnimatorController.Params.Slide);
            }
        }

    }

    public void OnSlide()
    {
        Speed = _slideSpeed;
    }
}
