using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputMap
{
    private const string DeviceKeyboard = "Keyboard";

    private Dictionary<string, string> _keyboardActions;

    public InputMap()
    {
        _keyboardActions = new Dictionary<string, string>();
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
        }
    }

    public IEnumerable<InputInfo> GetInputInfo(IEnumerable<string> keys)
    {
        foreach(var key in keys)
        {
            if (_keyboardActions.ContainsKey(key))
            {
                yield return new InputInfo(key, _keyboardActions[key]);
            }
            else
            {
                yield return new InputInfo(key, string.Empty);
            }
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