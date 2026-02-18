using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtils
{
    public static PowerUp GeneratePowerUpByPlayer(PlayerStatus status)
    {
        switch (status)
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
