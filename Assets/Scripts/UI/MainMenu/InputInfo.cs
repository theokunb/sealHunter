public class InputInfo
{
    public string Label { get; private set; }
    public string KeyboardLabel { get; private set; }

    public InputInfo(string label, string keyboardLabel)
    {
        Label = label;
        KeyboardLabel = keyboardLabel;
    }
}
