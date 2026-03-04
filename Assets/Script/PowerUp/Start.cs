public class Start : PowerUpBase
{
    protected override void Apply()
    {
        MainClass.Player.SetIsInvincible(true);
        SoundConst.PowerUp.Play();
        MainClass.Player.PlusPoint(PointsUtils.PowerUp);
    }
}
