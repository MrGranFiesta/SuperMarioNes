using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> ListPanel = new List<GameObject>();
    [SerializeField] private Level _level;
    private void Awake()
    {
        HidePanel();

        ListPanel[0].SetActive(true);
    }

    private void HidePanel()
    {
        foreach (var item in ListPanel)
        {
            item.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SoundConst.Beep.Play();
        MainClass.Player.SetLive(MainClass.Config.Dificult.LivesStarter);
        _level.LoadLevel();
    }

    public void OpenConfig()
    {
        SoundConst.Beep.Play();
        HidePanel();
        ListPanel[1].SetActive(true);
    }

    public void OpenPoints()
    {
        SoundConst.Beep.Play();
        HidePanel();
        ListPanel[2].SetActive(true);
    }

    public void OpenBack()
    {
        HidePanel();
        ListPanel[0].SetActive(true);
    }

    public void BackMenu()
    {
        SoundConst.Beep.Play();
        foreach (var item in ListPanel)
        {
            item.SetActive(false);
        }

        ListPanel[0].SetActive(true);
    }


}
