public class LanguajeCode
{
    public string code;
    public string InternationalizationKey;

    private LanguajeCode(string code)
    {
        this.code = code;
        InternationalizationKey = $"_{code}";
    }

    public static readonly LanguajeCode ES = new LanguajeCode("es");
    public static readonly LanguajeCode IN = new LanguajeCode("in");
}
