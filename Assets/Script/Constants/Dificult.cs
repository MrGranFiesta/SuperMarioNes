public class Dificult {
    public string InternationalizationKey { get; private set; }
    public int LivesStarter { get; private set; }

    private Dificult(string internationalizationKey, int livesStarter) {
        this.InternationalizationKey = internationalizationKey;
        this.LivesStarter = livesStarter;
    }

    public static readonly Dificult Easy = new Dificult("_easy", 10);
    public static readonly Dificult Medium = new Dificult("_medium", 5);
    public static readonly Dificult Hard = new Dificult("_hard", 3);
}
