using UnityEngine;

public class WallBlock : BlockBase
{
    [SerializeField] private BlockTypeModeWall _mode;
    [SerializeField] private PowerUp _content;
    private bool _isActived = false;

    public override void OnHit()
    {
        if (_isActived) return;
        _isActived = true;

        base.OnHit(_mode != BlockTypeModeWall.Void);


        switch (_mode)
        {
            case BlockTypeModeWall.Void:
                if (MainClass.Player.Status == PlayerStatus.Small) return;
                MainClass.Player.PlusPoint(PointsUtils.WallBlock);
                Destroy(gameObject);
                break;
            case BlockTypeModeWall.Coin:
                LaunchCoinAnimation();
                break;
            case BlockTypeModeWall.PowerUpAuto:
                GeneratePowerUp(PlayerUtils.GeneratePowerUpByPlayer(MainClass.Player.Status));
                break;
            case BlockTypeModeWall.PowerUpSpecific:
                GeneratePowerUp(_content);
                break;
        }
    }
}
