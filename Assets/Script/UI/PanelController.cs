using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject firstSelected;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}
