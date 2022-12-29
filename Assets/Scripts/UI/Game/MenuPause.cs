using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;

    private void Start()
    {
        _resumeButton.Select();
    }

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumeClicked);
        _exitButton.onClick.AddListener(OnExitClicked);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumeClicked);
        _exitButton.onClick.RemoveListener(OnExitClicked);
    }

    private void OnResumeClicked()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnExitClicked()
    {
        MainMenu.Load();
    }
}
