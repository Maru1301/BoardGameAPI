namespace BoardGameClient;

public class Character
{
    public string Name { get; set; } = string.Empty;
    public string Rule { get; set; } = string.Empty;
    public int[] Card { get; set; } = [];
    public string Disqualification { get; set; } = string.Empty;
    public string Evolution { get; set; } = string.Empty;
    public string AdditionalPoint { get; set; } = string.Empty;
}
