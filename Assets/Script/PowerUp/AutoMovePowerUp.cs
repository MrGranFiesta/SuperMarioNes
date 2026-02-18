using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class AutoMovePowerUp : AutoMove
{
    protected override void Awake()
    {
        base.Awake();
        _layerMask = LayerMask.GetMask(
            LayerUtils.Ground,
            LayerUtils.Default,
            LayerUtils.Block
        );
    }
}
