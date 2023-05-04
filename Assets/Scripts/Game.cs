using UnityEngine;
using DG.Tweening;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private SoundsContainer _soundsContainer;
    [SerializeField] private MenuPause _menuPause;
    [SerializeField] private GameOverZone _gameoverZone;
    [SerializeField] private EndGameScreen _endGameScreen;

    private PlayerInput _playerInput;

    private void Awake()
    {
        ResumeGame();
        _playerInput = new PlayerInput();

        _playerInput.UI.Pause.performed += context => OnPauseClicked();
    }

    private void OnEnable()
    {
        _playerShoot.PlayerShooted += OnPlayerShooted;
        _spawner.EnemySpawnedSound += OnEnemySpawnedSound;
        _spawner.GameWin += OnEndGame;
        _gameoverZone.GameOver += OnEndGame;
        _playerInput.Enable();
        _menuPause.ResumeClicked += OnResumeClicked;
    }

    private void OnDisable()
    {
        _playerShoot.PlayerShooted -= OnPlayerShooted;
        _spawner.EnemySpawnedSound -= OnEnemySpawnedSound;
        _spawner.GameWin -= OnEndGame;
        _gameoverZone.GameOver -= OnEndGame;
        _playerInput.Disable();
        _menuPause.ResumeClicked -= OnResumeClicked;
    }

    private void Start()
    {
        if (_soundsContainer.TryGetBackgroundAudioSource(out AudioSource audioSource))
        {
            audioSource.Play();
        }
    }

    private void OnPlayerShooted(AudioSource audioSource)
    {
        if (_soundsContainer.TryGetWeaponAudioSource(out AudioSource res))
        {
            res.PlayOneShot(audioSource.clip);
        }
    }

    private void OnEnemySpawnedSound(AudioSource audioSource)
    {
        if (audioSource == null || audioSource.clip == null)
            return;

        if (_soundsContainer.TryGetEnemyAudioSource(out AudioSource res))
        {
            res.PlayOneShot(audioSource.clip);
        }
    }

    public void OnPauseClicked()
    {
        if (_menuPause.gameObject.activeInHierarchy == true)
        {
            OnResumeClicked();
            _menuPause.gameObject.SetActive(false);
        }
        else
        {
            StopGame();
            _menuPause.gameObject.SetActive(true);
        }
    }

    private void OnEndGame(string title)
    {
        StopGame();
        _endGameScreen.FilleFields(title, _player.Score);
        _endGameScreen.gameObject.SetActive(true);
    }

    private void OnResumeClicked()
    {
        ResumeGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        _player.enabled = false;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        _player.enabled = true;
    }
}
