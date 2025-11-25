using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 _movePlayer;

    SpriteRenderer _sprite;

    [SerializeField]
    private float _speed = 0.1f;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        _movePlayer.x = Input.GetAxisRaw("Horizontal");
        _movePlayer.y = Input.GetAxisRaw("Vertical");

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
