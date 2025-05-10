using System;
using UnityEngine;

public class LeverOpen : MonoBehaviour
{

    public Sprite _sprite1;
    public Sprite _sprite2;

    private SpriteRenderer _spriteRenderer;

    public static Action OnStoneOpen;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer.sprite = _sprite1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            _spriteRenderer.sprite = _sprite2;
            OnStoneOpen?.Invoke();

            Debug.Log("HURRY UP! DOOR IS OPEN!");
        }
        else
            _spriteRenderer.sprite = _sprite1;
    }
}