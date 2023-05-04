using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class LeadersPanel : MonoBehaviour
{
    [SerializeField] private Button _authButton;
    [SerializeField] private Button _refreshButton;
    [SerializeField] private GameObject _header;
    [SerializeField] private Entry _entry;
    [SerializeField] private Transform _container;
    [SerializeField] private LocalizedString _defaultName;

    private void OnEnable()
    {
        _refreshButton.onClick.AddListener(Refresh);
        _authButton.onClick.AddListener(Auth);
    }

    private void OnDisable()
    {
        _refreshButton.onClick.RemoveListener(Refresh);
        _authButton.onClick.RemoveListener(Auth);
    }

    private void Start()
    {
        Refresh();
    }

    private void Auth()
    {
        PlayerAccount.RequestPersonalProfileDataPermission(() =>
        {
            Refresh();
        },
        (error) =>
        {
            Refresh();
        });
    }

    private void Refresh()
    {
        Clear();
        Show();
    }

    private void Clear()
    {
        foreach(Transform children in _container)
        {
            Destroy(children.gameObject);
        }
    }

    private void Show()
    {
        var header = Instantiate(_header, _container);

        Leaderboard.GetEntries(LeaderboardTables.BestPlayers, (response) =>
        {
            foreach(var entry in response.entries)
            {
                var createdEntry = Instantiate(_entry, _container);
                createdEntry.Render(entry.rank.ToString(), CorrectName(entry.player.publicName), entry.score.ToString());
            }
        });
    }

    private string CorrectName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return _defaultName.GetLocalizedString();
        }
        else
        {
            return name;
        }
    }
}
