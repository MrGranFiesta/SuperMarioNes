using UnityEngine;

public class InvisibleBlock : QuestionBlock
{
    private SpriteRenderer _sprite;
    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public override void OnHit()
    {   
        Col.isTrigger = false;
        _sprite.enabled = true;
        base.OnHit();
    }
}
