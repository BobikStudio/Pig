using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Singeltone;

    public int Score { get { return _score; } set { _score = value; OnScoreChanged?.Invoke(); } }
    private int _score = 0;

    private List<Stone> _stones = new List<Stone>();

    [HideInInspector] public UnityEvent OnScoreChanged;

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

    private void Start()
    {
        _stones.AddRange(FindObjectsOfType<Stone>());

        OnRestart();
        LevelController.Singeltone.OnLevelRestarted.AddListener(OnRestart);
    }

    private void OnRestart()
    { 
        UIController.Singeltone.SetActiveEndGamePanel(false);
        Score = 0;
    }

    public void CheckGameState ()
    {
        if (_stones.All(stone => stone.Active == false))
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        UIController.Singeltone.SetEndGamePanelState(true);
        UIController.Singeltone.SetActiveEndGamePanel(true);
    }

    public void LoseGame()
    {
        UIController.Singeltone.SetEndGamePanelState(false);
        UIController.Singeltone.SetActiveEndGamePanel(true);
    }
}
