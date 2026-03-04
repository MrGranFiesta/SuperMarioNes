using System.Collections.Generic;

public class Config
{
    public Dificult Dificult { get; set; } = Dificult.Medium;
    public string Languaje { get; private set; }
    public List<string> AvaibleLanguages { get; private set; } = new List<string>();
}