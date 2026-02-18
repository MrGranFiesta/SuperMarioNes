using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointsUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        MainClass.CustomEvents.OnCoinChanged.AddListener((coins) => {
            txt.text = $"x {coins.ToString("00")}";
        });
    }
}
