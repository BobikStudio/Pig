using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombButton : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private Image _fillImage;

    private void Start()
    {
        _bombSpawner.OnBombCooldownChanged.AddListener(RefreshButton);
        RefreshButton();
    }

    public void OnClick()
    {
        _bombSpawner.TryCreateBomb();
    }

    private void RefreshButton()
    {
        _fillImage.fillAmount = Mathf.InverseLerp(0, _bombSpawner.SpawnCooldown, _bombSpawner.TimeLeft);
    }
}
