using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputMap
{
    private const string DeviceKeyboard = "Keyboard";
    private const string DeviceGamepad = "Gamepad";

    private Dictionary<string, string> _keyboardActions;
    private Dictionary<string, string> _gamepadActions;

    public InputMap()
    {
        _keyboardActions = new Dictionary<string, string>();
        _gamepadActions = new Dictionary<string, string>();
    }

    public void ParseActions(IEnumerable<InputBinding> bindings)
    {
        foreach (var element in bindings)
        {
            string pattern = $"<{element.groups}>/";

            if (element.groups == DeviceKeyboard)
            {
                Add(element.action, element.path.Replace(pattern, ""), _keyboardActions);
            }
            else if (element.groups == DeviceGamepad)
            {
                Add(element.action, element.path.Replace(pattern, ""), _gamepadActions);
            }
        }
    }

    public IEnumerable<InputInfo> GetInputInfo()
    {
        foreach (var key in _keyboardActions.Keys)
        {
            yield return new InputInfo(key, _keyboardActions[key], _gamepadActions[key]);
        }
    }

    private void Add(string key, string value, Dictionary<string, string> collection)
    {
        if (collection.ContainsKey(key))
        {
            collection[key] += $"/{value}";
        }
        else
        {
            collection.Add(key, value);
        }
    }
}