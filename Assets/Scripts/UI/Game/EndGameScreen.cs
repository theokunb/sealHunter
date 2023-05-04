using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using UnityEngine.Localization;
using Agava.YandexGames;
using System.Linq;

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
        Leaderboard.GetPlayerEntry(LeaderboardTables.BestPlayers, (response) =>
        {
            string message = $"{_currentScore.GetLocalizedString()}: {currentScore}";

            if(response != null)
            {
                message += $"\n{_bestScore.GetLocalizedString()}: {response.score}";
            }

            if (response == null || response.score < currentScore)
            {
                Leaderboard.SetScore(LeaderboardTables.BestPlayers, currentScore);
            }

            _message.text = message;
        });
    }

    private void OnContinueButtonClicked()
    {
        MainMenu.Load();
    }
}
