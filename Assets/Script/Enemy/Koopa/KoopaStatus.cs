public abstract class KoopaStatus
{
    private KoopaStatus() { }

    public sealed class Walking : KoopaStatus { }
    public sealed class Shield : KoopaStatus { }
    public sealed class ShieldInvulnerable : KoopaStatus { }
    public sealed class ShieldTryGetUp : KoopaStatus { }

    public static readonly KoopaStatus WalkingState = new Walking();
    public static readonly KoopaStatus ShieldState = new Shield();
    public static readonly KoopaStatus ShieldInvulnerableState = new ShieldInvulnerable();
    public static readonly KoopaStatus ShieldTryGetUpState = new ShieldTryGetUp();

    public bool IsWalking() => this is Walking;
    public bool IsShieldNotInvulnerable() => this is Shield || this is ShieldTryGetUp;
    public bool IsShieldInvulnerable() => this is ShieldInvulnerable;
    public bool IsShieldTryGetUp() => this is ShieldTryGetUp;
    public bool IsOnlyShield() => this is Shield;


}