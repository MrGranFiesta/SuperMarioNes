using UnityEngine;

public class QuestionBlock : BlockBase
{
    [SerializeField] private BlockTypeModeQuestion _mode;
    [SerializeField] private PowerUp _content;
    private bool _isActived = false;
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnHit()
    {
        if (_isActived) return;
        _isActived = true;

        base.OnHit(true);
        switch (_mode)
        {
            case BlockTypeModeQuestion.Coin:
                LaunchCoinAnimation();
                break;
            case BlockTypeModeQuestion.PowerUpAuto:
                GeneratePowerUp(PlayerUtils.GeneratePowerUpByPlayer(MainClass.Player.Status));
                break;
            case BlockTypeModeQuestion.PowerUpSpecific:
                GeneratePowerUp(_content);
                break;
        }

    }
}
