public class InputInfo
{
    public string Label { get; private set; }
    public string KeyboardLabel { get; private set; }
    public string GamepadLabel { get; private set; }

    public InputInfo(string label, string keyboardLabel, string gamepadLabel)
    {
        Label = label;
        KeyboardLabel = keyboardLabel;
        GamepadLabel = gamepadLabel;
    }
}
