public class Champinion : PowerUpBase
{
    protected override void Apply()
    {
        MainClass.Player.PlusPoint(PointsUtils.PowerUp);
        SoundConst.PowerUp.Play();

        if (MainClass.Player.Status == PlayerStatus.Fire ||
            MainClass.Player.Status == PlayerStatus.Big
        ) {
            return; 
        }

        MainClass.Player.SetStatusPlayer(PlayerStatus.Big);
    }
}
