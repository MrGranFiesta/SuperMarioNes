using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GetPointsPanelControllerUI : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> txtPoints = new List<TextMeshProUGUI>();

    private void OnEnable()
    {
        GetPoints();
    }

    private void GetPoints()
    {
        List<int> points = MainClass.Datastore.GetPoints().OrderByDescending(p => p).ToList();

        for (int i = 0; i < txtPoints.Count; i++)
        {
            if (i < points.Count)
            {
                txtPoints[i].text = points[i].ToString("000000");
            }
            else
            {
                txtPoints[i].text = "000000";
            }
        }
    }
}
