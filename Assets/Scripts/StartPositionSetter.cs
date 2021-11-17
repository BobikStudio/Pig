using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionSetter : MonoBehaviour
{
    Vector2 _startPosition;

    private void Start()
    {
        _startPosition = gameObject.transform.position;
        LevelController.Singeltone.OnLevelRestarted.AddListener(SetStartPosition);
    }

    private void SetStartPosition()
    {
        transform.position = _startPosition;
    }
}
