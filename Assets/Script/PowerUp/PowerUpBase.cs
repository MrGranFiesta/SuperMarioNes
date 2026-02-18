using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void Apply();

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            Apply();
            Destroy(gameObject);
        }
    }
}
