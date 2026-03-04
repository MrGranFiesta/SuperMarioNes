using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MenuManagerLegacy : MonoBehaviour
{
    public static MenuManagerLegacy _instance;

    [SerializeField] private List<OptionBase> optionsStart = new List<OptionBase>();
    [SerializeField] private List<OptionBase> optionsConfig = new List<OptionBase>();
    [SerializeField] private List<OptionBase> optionsPoints = new List<OptionBase>();

    [SerializeField] private List<TextMeshProUGUI> txtPoints = new List<TextMeshProUGUI>();
    private Screems screem = Screems.Start;

    [SerializeField] private GameObject fatherStart;
    [SerializeField] private GameObject fatherConfig;
    [SerializeField] private GameObject fatherPoints;

    private int _indexOption = 0;
    private UnityEvent OnChangeIndex = new UnityEvent();

    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        InputManager.InputActions.UI.Up.performed += PreviosOption;
        InputManager.InputActions.UI.Down.performed += NextOption;
        InputManager.InputActions.UI.Enter.performed += EnterOption;
        InputManager.InputActions.UI.Back.performed += BackOption;
        OnChangeIndex.AddListener(UpdateIndex);
    }

    private void Start()
    {
        GetOptionByIndexAndStatus().OnSelect();
    }

    private void UpdateIndex()
    {
        foreach (var item in GetListOptionByStatus())
        {
            item.OnDeselect();
        }
        GetOptionByIndexAndStatus().OnSelect();
    }

    public void ChangeScreem(Screems screem)
    {
        this.screem = screem;
        fatherStart.SetActive(false);
        fatherConfig.SetActive(false);
        fatherPoints.SetActive(false);
        switch (this.screem)
        {
            case Screems.Start:
                Debug.Log("Start");
                fatherStart.SetActive(true);
                break;
            case Screems.Config:
                Debug.Log("Config");
                fatherConfig.SetActive(true);
                break;
            case Screems.Points:
                Debug.Log("Points");
                fatherPoints.SetActive(true);
                GetPoints();
                break;
        }
        _indexOption = 0;
        OnChangeIndex.Invoke();
    }


    private void NextOption(InputAction.CallbackContext context)
    {
        SoundConst.Beep.Play();
        if (_indexOption < optionsStart.Count - 1)
        {
            _indexOption++;
            OnChangeIndex.Invoke();
        }
    }

    private void PreviosOption(InputAction.CallbackContext context)
    {
        SoundConst.Beep.Play();
        if (_indexOption > 0)
        {
            _indexOption--;
            OnChangeIndex.Invoke();
        }
    }

    private void EnterOption(InputAction.CallbackContext context)
    {
        GetOptionByIndexAndStatus().OnEnter();
    }

    private void BackOption(InputAction.CallbackContext context)
    {
        GetOptionByIndexAndStatus().OnBack();
    }

    private OptionBase GetOptionByIndexAndStatus()
    {
        switch (screem)
        {
            case Screems.Start:
                return optionsStart[_indexOption];
            case Screems.Config:
                return optionsConfig[_indexOption];
            case Screems.Points:
                return optionsPoints[_indexOption];
        }
        return null;
    }

    private List<OptionBase> GetListOptionByStatus()
    {
        switch (screem)
        {
            case Screems.Start:
                return optionsStart;
            case Screems.Config:
                return optionsConfig;
            case Screems.Points:
                return optionsPoints;
        }
        return null;
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
