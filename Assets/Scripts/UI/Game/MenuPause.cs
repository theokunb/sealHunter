using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using System;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;

    public event Action ResumeClicked;

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
        ResumeClicked?.Invoke();
    }

    private void OnExitClicked()
    {
        MainMenu.Load();
    }
}
