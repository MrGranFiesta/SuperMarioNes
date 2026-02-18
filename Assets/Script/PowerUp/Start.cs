using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : PowerUpBase
{
    protected override void Apply()
    {
        MainClass.Player.SetIsInvincible(true);
        MainClass.Player.PlusPoint(PointsUtils.PowerUp);
    }
}
