using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ToPointMove))]
[RequireComponent(typeof(FieldOfView))]
[RequireComponent(typeof(SpriteController))]

public class DogAI : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacles;
    [SerializeField] [Min(0)] private float _unitTriggerSize;

    private ToPointMove _movement;
    private DogState _state;

    private FieldOfView      _fieldOfView;
    private SpriteController _spriteController;

    private Vector2 _movePoint;

    private void Start()
    {
        _movement = GetComponent<ToPointMove>();
        _fieldOfView = GetComponent<FieldOfView>();
        _spriteController = GetComponent<SpriteController>();

        OnRestart();
        LevelController.Singeltone.OnLevelRestarted.AddListener(OnRestart);
    }

    private void FixedUpdate()
    {
         StateControl();
        _movement.Move(_movePoint);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            GameManager.Singeltone.LoseGame();
        }
    }

    private void OnRestart()
    {
        _movePoint = transform.position;
        SetState(DogState.Patrol);
    }

    public void SetState(DogState state)
    {
        _state = state;

        switch (state)
        {
            case DogState.Attack:
                _spriteController.SetSprite("Attack");
                _movement.SetMovementSpeed(2);
                break;
            case DogState.Idle:
                _spriteController.SetSprite("Idle");
                _movement.SetMovementSpeed(0);
                break;
            case DogState.Patrol:
                _spriteController.SetSprite("Patrol");
                _movement.SetMovementSpeed(0.8f);
                break;
        }
    }

    public void ExploseDog()
    {
        GameManager.Singeltone.Score += 1000;
        SetState(DogState.Idle);
        StartCoroutine(IdleWaitTime());
    }

    private void StateControl()
    {
        switch(_state)
        {
            case DogState.Patrol:
                if (_fieldOfView.VisibleTargets.Count > 0)
                {
                    SetState(DogState.Attack);
                }
                else if (Vector2.Distance(transform.position, _movePoint) < _unitTriggerSize)
                {
                    _movePoint = GetRandomMovePoint();
                }
                break;

            case DogState.Attack:

                if (_fieldOfView.VisibleTargets.Count > 0)
                {
                    _movePoint = _fieldOfView.VisibleTargets[0].transform.position;
                }
                else if (Vector2.Distance(transform.position, _movePoint) < _unitTriggerSize)
                {
                    SetState(DogState.Patrol);
                }

                break;

            case DogState.Idle:
                _movePoint = transform.position;
                break;
        }
    }

    private Vector2 GetRandomMovePoint ()
    {
        Vector2 movePoint = (Vector2)transform.position + new Vector2(Random.Range(-2, 2f), Random.Range(-2, 2f));
        if (Physics2D.Linecast(transform.position, movePoint, _obstacles))
        {
            movePoint = GetRandomMovePoint();
        }

        return movePoint;
    }

    private IEnumerator IdleWaitTime()
    {
        yield return new WaitForSeconds(4f);
        SetState(DogState.Patrol);
    }

    public enum DogState
    { 
        Patrol,
        Idle,
        Attack
    }
}
