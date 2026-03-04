public class SoundConst
{
    public string Value { get; }

    private SoundConst(string value)
    {
        Value = value;
    }

    public void Play()
    {
        MainClass.AudioManager.AudioPooling.PlaySound(ResourceManager.GetClip(this));
    }

    //Sound
    public static readonly SoundConst LifeUp = new SoundConst("1up");
    public static readonly SoundConst Beep = new SoundConst("beep");
    public static readonly SoundConst BillFirework = new SoundConst("billfirework");
    public static readonly SoundConst Brick = new SoundConst("brick");
    public static readonly SoundConst Bump = new SoundConst("bump");
    public static readonly SoundConst Coin = new SoundConst("coin");
    public static readonly SoundConst Death = new SoundConst("death");
    public static readonly SoundConst FireBall = new SoundConst("fireball");
    public static readonly SoundConst FlagPole = new SoundConst("flagpole");
    public static readonly SoundConst GameOver = new SoundConst("gameover");
    public static readonly SoundConst HurryUp = new SoundConst("hurryup");
    public static readonly SoundConst Item = new SoundConst("item");
    public static readonly SoundConst Jump = new SoundConst("jump");
    public static readonly SoundConst JumpSmall = new SoundConst("jumpsmall");
    public static readonly SoundConst KickKill = new SoundConst("kickKill");
    public static readonly SoundConst Pause = new SoundConst("pause");
    public static readonly SoundConst PipePowerDown = new SoundConst("pipepowerdown");
    public static readonly SoundConst PowerUp = new SoundConst("powerup");
    public static readonly SoundConst Stompswim = new SoundConst("stompswim");
}
