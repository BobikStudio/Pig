using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ToPointMove : MonoBehaviour, IMove
{
    [SerializeField] [Min(0)] private float _movementSpeed = 1;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetMovementSpeed(float moveSpeed)
    {
        _movementSpeed = moveSpeed;
    }

    public void Move(Vector2 point)
    {
        if (Vector2.Distance(transform.position, point) > 0.1f)
        {
            Vector2 direction = (point - (Vector2)transform.position).normalized;
            _rigidbody2D.velocity = direction * _movementSpeed;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }    
    }
}
