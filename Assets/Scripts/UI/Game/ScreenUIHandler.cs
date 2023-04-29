using DG.Tweening;
using UnityEngine;

public class ScreenUIHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _screenUI;
    [SerializeField] private float _fadeTime;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.AnyKeyboard.performed += OnKeyboard;
        _playerInput.Player.AnyTouch.performed += AnyTouch;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.AnyKeyboard.performed -= OnKeyboard;
        _playerInput.Player.AnyTouch.performed -= AnyTouch;
        _playerInput.Disable();
    }

    private void OnKeyboard(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _screenUI.DOFade(0, _fadeTime);
    }

    private void AnyTouch(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _screenUI.DOFade(1, _fadeTime);
    }
}
