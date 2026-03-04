using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointUIController : MonoBehaviour
{
    private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        FormatedTxt(MainClass.Player.Points);
        MainClass.CustomEvents.OnPointsChanged.AddListener((points) => {
            FormatedTxt(points);
        });
    }

    private void FormatedTxt(int points)
    {
        txt.text = points.ToString("000000");
    }
}
