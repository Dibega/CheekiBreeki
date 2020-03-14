public class GameModel
{
    public GameData GetGameData(uint number)
    {
        return new GameData(number, TextByNumber(number));
    }

    private string TextByNumber(uint number)
    {
        string text = "";

        if (number % 3 == 0)
            text += "Cheeki";
        if (number % 5 == 0)
            text += "Breeki";
        if (string.IsNullOrEmpty(text))
            text += number.ToString();

        return text;
    }


}
