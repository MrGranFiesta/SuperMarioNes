using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverScreemManager : MonoBehaviour
{
    [SerializeField] private GameObject _partyOver;
    [SerializeField] private TextMeshProUGUI _gameOverTxt;
    [SerializeField] private Level GoToMenu;
    [SerializeField] private Level GoToOverwold;
    void Awake()
    {
        if (MainClass.Player.Live == 0) {
            _gameOverTxt.text = "GAME OVER";
            SoundConst.GameOver.Play();
            StartCoroutine(LoadScene(GoToMenu));
        } else
        {
            _partyOver.SetActive(true);
            StartCoroutine(LoadScene(GoToOverwold));
        }
    }

    private IEnumerator LoadScene(Level level)
    {
        yield return new WaitForSeconds(GameConstants.GameOverTimeScreem);
        level.LoadLevel();
    }
}
