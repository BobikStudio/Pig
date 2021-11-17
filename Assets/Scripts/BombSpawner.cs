using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _createPoint;

    [Min(0)] public float SpawnCooldown;

    [HideInInspector] public float TimeLeft;

    [HideInInspector] public UnityEvent OnBombCooldownChanged;

    private bool _bombReady = true;

    private void Start()
    {
        LevelController.Singeltone.OnLevelRestarted.AddListener(OnRestart);
    }

    private void OnRestart()
    {
        StopAllCoroutines();

        TimeLeft = 0;
        _bombReady = true;

        OnBombCooldownChanged?.Invoke();
    }

    public void TryCreateBomb()
    {
        if (_bombReady)
        {
            CreateBomb(_createPoint);
        }
    }

    private void CreateBomb(Transform transform)
    {
        CreateBomb(transform.position);
    }

    private void CreateBomb(Vector3 position)
    {
        Instantiate(_bombPrefab, position, Quaternion.identity);
        StartCoroutine(BombCooldown(SpawnCooldown));
    }

    private IEnumerator BombCooldown(float delay)
    {
        _bombReady = false;
        TimeLeft = delay;

        while (TimeLeft > 0)
        {
            TimeLeft -= 0.1f;
            OnBombCooldownChanged?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
        _bombReady = true;

    }
}
