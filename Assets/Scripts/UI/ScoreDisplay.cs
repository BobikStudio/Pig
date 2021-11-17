using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _display;

    private void Start()
    {
        GameManager.Singeltone.OnScoreChanged.AddListener(RefreshScoreDisplay);
    }

    private void OnEnable()
    {
        RefreshScoreDisplay();
    }


    private void RefreshScoreDisplay()
    {
        if (GameManager.Singeltone != null)
        {
            _display.text = "Score: " + GameManager.Singeltone.Score;
        }
    }
}
