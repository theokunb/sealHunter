using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _switchButton;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Game _game;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPause);
        _buyButton.onClick.AddListener(OnBuy);
        _switchButton.onClick.AddListener(OnSwitch);
        _reloadButton.onClick.AddListener(OnReload);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPause);
        _buyButton.onClick.RemoveListener(OnBuy);
        _switchButton.onClick.RemoveListener(OnSwitch);
        _reloadButton.onClick.RemoveListener(OnReload);
    }

    private void OnPause()
    {
        _game.OnPauseClicked();
    }

    private void OnBuy()
    {
        _player.OnBuy();
    }

    private void OnSwitch()
    {
        _player.SwitchWeapon();
    }

    private void OnReload()
    {
        _player.OnReload();
    }
}