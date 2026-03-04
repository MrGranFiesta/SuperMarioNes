using UnityEngine;

public class WallBlockMultiCoin : BlockBase
{
    private int _coinCount = 5;
    [SerializeField] private Sprite _emptyBlockSprite;

    public override void OnHit()
    {
        if (_coinCount <= 0)
        {
            SoundConst.Bump.Play();
            return;
        }
        _coinCount--;
        base.OnHit(_coinCount <= 0);
        LaunchCoinAnimation();
    }
}
