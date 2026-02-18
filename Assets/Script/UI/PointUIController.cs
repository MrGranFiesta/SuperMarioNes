using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointUIController : MonoBehaviour
{
    private TextMeshProUGUI txt;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        MainClass.CustomEvents.OnPointsChanged.AddListener((points) => {
            txt.text = points.ToString("000000");
        });
    }
}
