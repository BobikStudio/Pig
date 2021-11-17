using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    public bool Active;

    private void Start()
    {
        OnRestart();
        LevelController.Singeltone.OnLevelRestarted.AddListener(OnRestart);
    }

    private void OnEnable()
    {
        OnRestart();
    }

    public void ExploseStone()
    {
        GameManager.Singeltone.Score += 500;

        gameObject.SetActive(false);
        Active = false;

        GameManager.Singeltone.CheckGameState();
    }

    private void OnRestart()
    {
        gameObject.SetActive(true);
        Active = true;
    }
}
