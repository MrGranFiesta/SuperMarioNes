using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AnimationUtils
{
    public static void MoveTowards(GameObject go, Vector3 posOrigin, Vector3 posTarget, float velocity)
    {
        go.transform.position = Vector3.MoveTowards(
            posOrigin,
            posTarget,
            velocity * Time.deltaTime
        );
    }
}
