using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayGameUIController : OptionBase
{
    [SerializeField] private Level _level;

    public override void OnEnter()
    {
        MainClass.Player.SetLive(MainClass.Config.Dificult.LivesStarter);
        _level.LoadLevel();
    }

    public override void OnBack()
    {
        //Nothing to do
    }
}
