using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Slider _slider;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.LevelChanged += OnLevelChanged;
        _spawner.ProgressChanged += OnProgressChanged;
    }

    private void OnDisable()
    {
        _spawner.LevelChanged -= OnLevelChanged;
        _spawner.ProgressChanged -= OnProgressChanged;
    }

    private void OnLevelChanged(string label)
    {
        _label.text = label;
    }

    private void OnProgressChanged(float progress)
    {
        _slider.value = progress;
    }
}
