using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private TextMeshPro _text;
    [SerializeField] private float _speed;
    [SerializeField] private float _fadingSpeed;
    [SerializeField] private float _colorFadingSpeed;

    [SerializeField] private float _lifeTime;

    private float _textTimer;

    private void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _textTimer = _lifeTime;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(transform.position.x, transform.position.y + 1), _speed * Time.deltaTime);
        _textTimer -= Time.deltaTime;
        if (_textTimer < 0)
        {
            float alpha = _text.color.a - _colorFadingSpeed * Time.deltaTime;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
            if (_text.color.a < 50)
            {
                _speed = _fadingSpeed;
            }
            if (_text.color.a <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
