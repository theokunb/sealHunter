using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subpanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _nextButton;

    private void OnEnable()
    {
        _closeButton.Select();
        _closeButton.onClick.AddListener(OnCloseSelected);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnCloseSelected);
    }

    private void OnCloseSelected()
    {
        _nextButton.Select();
        gameObject.SetActive(false);
    }
}
