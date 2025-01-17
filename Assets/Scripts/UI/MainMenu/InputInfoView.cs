using TMPro;
using UnityEngine;

public class InputInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _keyboard;

    public string Key { get; private set; }

    public void Render(InputInfo inputInfo)
    {
        _label.text = inputInfo.Label;
        _keyboard.text = inputInfo.KeyboardLabel;

        Key = inputInfo.KeyboardLabel;
    }
}
