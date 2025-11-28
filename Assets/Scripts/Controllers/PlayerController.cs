using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector3 _movePlayer;

    SpriteRenderer _sprite;
    public FloatingJoystick _joystick;

    [SerializeField]
    private float _speed = 0.1f;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        _movePlayer.x = _joystick.Horizontal;
        _movePlayer.y = _joystick.Vertical;

        Vector3 dir = _movePlayer.normalized * _speed * Time.fixedDeltaTime;

        transform.position += dir;

        //ÁÂ¿ì º¯È¯
        Cheange();
    }

    private void Cheange()
    {
        if (_movePlayer.x < 0) _sprite.flipX = false;
        else if (_movePlayer.x > 0) _sprite.flipX = true;
    }
}
