public class PlayerUtils
{
    public static PowerUp GeneratePowerUpByPlayer()
    {
        switch (MainClass.Player.Status)
        {
            case PlayerStatus.Small:
                return PowerUp.Champinion;
            case PlayerStatus.Big:
                return PowerUp.Flower;
            case PlayerStatus.Fire:
                return PowerUp.Flower;
            default:                 
                return PowerUp.Champinion;
        }
    }
}
