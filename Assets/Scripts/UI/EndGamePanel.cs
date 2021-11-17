using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _stateDisplay;

    public void SetEndGamePanelState (bool isWin)
    {
        if (isWin)
        {
            _stateDisplay.text = "You win!";
        }
        else
        {
            _stateDisplay.text = "You lose!";
        }
    }
}
