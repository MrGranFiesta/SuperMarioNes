public class Flower : PowerUpBase
{
    protected override void Apply()
    {
        MainClass.Player.PlusPoint(PointsUtils.PowerUp);
        SoundConst.PowerUp.Play();
        if (MainClass.Player.Status == PlayerStatus.Fire)
        {
            return;
        }

        MainClass.Player.SetStatusPlayer(PlayerStatus.Fire);
    }
}
