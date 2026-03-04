using UnityEngine;

public class WallBlock : BlockBase
{
    [SerializeField] private BlockTypeModeWall _mode;
    [SerializeField] private PowerUp _content;

    public override void OnHit()
    {
        if (IsDisableOnHit) {
            SoundConst.Bump.Play();
            return;
        }
        IsDisableOnHit = true;

        base.OnHit(_mode != BlockTypeModeWall.Void);


        switch (_mode)
        {
            case BlockTypeModeWall.Void:
                if (MainClass.Player.Status == PlayerStatus.Small) return;
                MainClass.Player.PlusPoint(PointsUtils.WallBlock);
                SoundConst.Brick.Play();
                Destroy(gameObject);
                break;
            case BlockTypeModeWall.Coin:
                LaunchCoinAnimation();
                break;
            case BlockTypeModeWall.PowerUpAuto:
                GeneratePowerUp(PlayerUtils.GeneratePowerUpByPlayer());
                break;
            case BlockTypeModeWall.PowerUpSpecific:
                GeneratePowerUp(_content);
                break;
        }
    }
}
