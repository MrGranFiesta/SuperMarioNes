using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LiveUIController : MonoBehaviour
{
    private TextMeshProUGUI _txt;
    private void Awake()
    {
        _txt = GetComponent<TextMeshProUGUI>();
        _txt.text = MainClass.Player.Live.ToString();
    }
}
