using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IMove))]
public class InputMovementController : MonoBehaviour
{
    private IMove _movementType;

    private void Start()
    {
        _movementType = GetComponent<IMove>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 movePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _movementType.Move(movePosition);
        }
        else
        {
            _movementType.Move(transform.position);
        }
    }
}
