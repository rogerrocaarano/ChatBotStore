namespace TelegramBot.Types;

internal class KeyboardButton
{
    public string Text { get; private set; }
    public bool RequestLocation { get; private set; }

    private KeyboardButton(string text)
    {
        Text = text;
        RequestLocation = false;
    }

    public static KeyboardButton WithText(string text)
    {
        return new KeyboardButton(text);
    }

    public KeyboardButton WithRequestLocation()
    {
        RequestLocation = true;
        return this;
    }
}
