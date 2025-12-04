using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector3 _movePlayer;

    SpriteRenderer _sprite;
    public FloatingJoystick _joystick;


    private void Update()
    {
        _movePlayer.x = _joystick.Horizontal;
        _movePlayer.y = _joystick.Vertical;

        Vector3 dir = _movePlayer.normalized * _speed * Time.fixedDeltaTime;

        transform.position += dir;

        //ÁÂ¿ì º¯È¯
        GetComponent<SpriteRenderer>().flipX = _movePlayer.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if (target == null) 
            return;
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        Debug.Log($"OnDamaged ! {Hp}");

        // TEMP
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }
}
