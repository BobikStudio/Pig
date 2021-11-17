using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _explosionTime;
    [SerializeField] private float _explosionRadius;

    [Header("Initialization")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float _timeLeft;

    private void Start()
    {
        LevelController.Singeltone.OnLevelRestarted.AddListener(OnRestart);
        StartCoroutine(BombTimer());
    }

    private void Update()
    {
        _spriteRenderer.color = Color.Lerp(Color.red, Color.white, Mathf.InverseLerp(0, _explosionTime, _timeLeft));
    }

    private void OnRestart()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void Explose()
    {
        Collider2D [] collidersExplose = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (Collider2D collider in collidersExplose)
        {
            if (collider.gameObject.GetComponent<DogAI>())
            {
                collider.gameObject.GetComponent<DogAI>().ExploseDog();
            }

            if (collider.gameObject.GetComponent<Stone>())
            {
                collider.gameObject.GetComponent<Stone>().ExploseStone();
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator BombTimer()
    {
        _timeLeft = _explosionTime;
        while (_timeLeft > 0)
        {
            _timeLeft -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        Explose();
    }
}
