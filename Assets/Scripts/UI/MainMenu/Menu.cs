using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _rulesButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _rulesPanel;

    private void Start()
    {
        _playButton.Select();
    }

    private void OnEnable()
    {
        _rulesButton.onClick.AddListener(OnRulesButtonSelected);
        _playButton.onClick.AddListener(OnPlayButtonSelected);
        _quitButton.onClick.AddListener(OnQuitButtonSelected);
    }

    private void OnDisable()
    {
        _rulesButton.onClick.RemoveListener(OnRulesButtonSelected);
        _playButton.onClick.RemoveListener(OnPlayButtonSelected);
        _quitButton.onClick.RemoveListener(OnQuitButtonSelected);
    }

    private void OnPlayButtonSelected()
    {
        GameScene.Load();
    }

    private void OnRulesButtonSelected()
    {
        _rulesPanel.SetActive(true);
    }

    private void OnQuitButtonSelected()
    {
        Application.Quit();
    }
}
