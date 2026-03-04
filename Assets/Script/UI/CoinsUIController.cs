using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointsUIController : MonoBehaviour
{
    private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        FormatedTxt(MainClass.Player.Coins);
        MainClass.CustomEvents.OnCoinChanged.AddListener((coins) => {
            FormatedTxt(coins);
        });
    }

    private void FormatedTxt(int coins)
    {
        txt.text = $"x {coins.ToString("00")}";
    }
}
