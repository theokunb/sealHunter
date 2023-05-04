using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    private const float SlideFactor = 0.994f;

    [SerializeField] private float _speed;
    
    private Animator _animator;
    private Rigidbody2D _rigibody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigibody = GetComponent<Rigidbody2D>();
    }

    public void Move(PlayerInput playerInput)
    {
        Vector2 _moveDirection = playerInput.Player.Move.ReadValue<Vector2>();
        OnMove(_moveDirection);
    }

    private void OnMove(Vector2 direction)
    {
        _animator.SetFloat(PlayerAnimationController.Params.Speed, direction.sqrMagnitude);

        if (direction.sqrMagnitude < PlayerAnimationController.Params.WalkEps)
        {
            _rigibody.velocity *= SlideFactor;
            return;
        }

        _rigibody.velocity += direction * _speed * Time.deltaTime;
    }
}
