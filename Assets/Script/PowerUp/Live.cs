public class Live : PowerUpBase
{
    protected override void Apply()
    {
        MainClass.Player.PlusLive();
        Destroy(gameObject);
    }
}
