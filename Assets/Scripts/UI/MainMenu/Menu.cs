using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _rulesButton;
    [SerializeField] private GameObject _rulesPanel;
    [SerializeField] private GameObject _leaderboard;

    private void Start()
    {
        _playButton.Select();
    }

    private void OnEnable()
    {
        _rulesButton.onClick.AddListener(OnRulesButtonSelected);
        _playButton.onClick.AddListener(OnPlayButtonSelected);
        _leaderboardButton.onClick.AddListener(OnLeaderboardButtonSelected);
    }

    private void OnDisable()
    {
        _rulesButton.onClick.RemoveListener(OnRulesButtonSelected);
        _playButton.onClick.RemoveListener(OnPlayButtonSelected);
        _leaderboardButton.onClick.RemoveListener(OnLeaderboardButtonSelected);
    }

    private void OnPlayButtonSelected()
    {
        GameScene.Load();
    }

    private void OnRulesButtonSelected()
    {
        _rulesPanel.SetActive(true);
    }

    private void OnLeaderboardButtonSelected()
    {
        _leaderboard.SetActive(true);
    }
}
