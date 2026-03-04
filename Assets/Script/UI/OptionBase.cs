
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class OptionBase : MonoBehaviour
{
    protected TextMeshProUGUI Txt { get; set; }

    protected virtual void Awake()
    {
        Txt = GetComponent<TextMeshProUGUI>();
    }

    public void OnSelect()
    {
        Txt.color = Color.yellow;
    }

    public void OnDeselect()
    {
        Txt.color = Color.white;
    }

    public abstract void OnEnter();
    public abstract void OnBack();
}
