using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private SoundsContainer _soundsContainer;
    [SerializeField] private GameObject _menuPause;
    [SerializeField] private GameOverZone _gameoverZone;
    [SerializeField] private EndGameScreen _endGameScreen;

    private PlayerInput _playerInput;

    private void Awake()
    {
        Time.timeScale = 1;
        _playerInput = new PlayerInput();

        _playerInput.UI.Pause.performed += context => OnPauseClicked();
    }

    private void OnEnable()
    {
        _player.PlayerShooted += OnPlayerShooted;
        _spawner.EnemySpawnedSound += OnEnemySpawnedSound;
        _spawner.GameWin += OnEndGame;
        _gameoverZone.GameOver += OnEndGame;
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _player.PlayerShooted -= OnPlayerShooted;
        _spawner.EnemySpawnedSound -= OnEnemySpawnedSound;
        _spawner.GameWin -= OnEndGame;
        _gameoverZone.GameOver -= OnEndGame;
        _playerInput.Disable();
    }

    private void Start()
    {
        if(_soundsContainer.TryGetBackgroundAudioSource(out AudioSource audioSource))
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

    private void OnPauseClicked()
    {
        if(_menuPause.activeInHierarchy == true)
        {
            Time.timeScale = 1;
            _menuPause.SetActive(false);
            _soundsContainer.UnPauseAll();
        }
        else
        {
            Time.timeScale = 0;
            _menuPause.SetActive(true);
            _soundsContainer.PauseGameSounds();
        }
    }

    private void OnEndGame(string title)
    {
        Time.timeScale = 0;
        _endGameScreen.FilleFields(title, _player.Score);
        _endGameScreen.gameObject.SetActive(true);
    }
}
