using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OptionNavigate : OptionBase
{
    [SerializeField] private Screems _navigateScreem;

    public override void OnEnter()
    {
        Debug.Log("Navigate to " + _navigateScreem + " aaa " + gameObject.name);
        MenuManagerLegacy._instance.ChangeScreem(_navigateScreem);
    }

    public override void OnBack()
    {
        //Nothing to do
    }
}
