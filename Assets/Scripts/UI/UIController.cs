using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController Singeltone;

    [SerializeField] private EndGamePanel _endGamePanel;
    [SerializeField] private GameObject   _HUD;

    private void Awake()
    {
        if (Singeltone == null)
        {
            Singeltone = this;
        }
        else if (Singeltone != this)
        {
            Destroy(this);
        }
    }

    public void SetActiveEndGamePanel(bool value)
    {
        _endGamePanel.gameObject.SetActive(value);
        SetActiveHUD(!value);

        if (value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void SetEndGamePanelState(bool isWin)
    {
        _endGamePanel.SetEndGamePanelState(isWin);
    }


    public void SetActiveHUD(bool value)
    {
        _HUD.SetActive(value);
    }
}
