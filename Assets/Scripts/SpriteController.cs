using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteController : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _flipToMoveDirection;

    [SerializeField] List<SpriteStateContainer> _stateList = new List<SpriteStateContainer>();

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        if (_flipToMoveDirection)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        if (_flipToMoveDirection)
        {
            if (_rigidbody2D.velocity.x < 0)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }
    }

    public Sprite GetStateSprite(string stateName)
    {
        if (_stateList.Any(state => state.StateName == stateName))
        {
            return _stateList.Where(state => state.StateName == stateName).First().StateSprite;
        }
        else
        {
            Debug.LogError("State does not exist! " + stateName);
            return null;
        }
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void SetSprite(string stateName)
    {
        _spriteRenderer.sprite = GetStateSprite(stateName);
    }

    [System.Serializable]
    public class SpriteStateContainer
    {
        public string StateName;
        public Sprite StateSprite;
    }
}


