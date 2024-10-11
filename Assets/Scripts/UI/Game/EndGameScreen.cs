using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using UnityEngine.Localization;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using System.Text;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private Button _continueButton;
    [SerializeField] private LocalizedString _currentScore;
    [SerializeField] private LocalizedString _bestScore;

    private void OnEnable()
    {
        _continueButton.Select();
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
    }

    public void FilleFields(string title, int score)
    {
        _title.text = title;
        SetMessage(score);
    }

    private void SetMessage(int currentScore)
    {
        var score = PlayerPrefs.GetInt(Constants.Strings.Score, 0);
        StringBuilder message = new StringBuilder();

        message.Append($"{_currentScore.GetLocalizedString()}: {currentScore}");

        if(score != 0)
        {
            message.Append($"\n{_bestScore.GetLocalizedString()}: {score}");
        }
        
        if(score == 0 || score < currentScore)
        {
            PlayerPrefs.SetInt(Constants.Strings.Score, currentScore);
        }
        _message.text = message.ToString();
    }

    private void OnContinueButtonClicked()
    {
        MainMenu.Load();
    }
}
