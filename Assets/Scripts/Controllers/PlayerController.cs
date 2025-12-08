using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : CreatureController
{
    Vector3 _movePlayer;

    SpriteRenderer _sprite;
    public FloatingJoystick _joystick;

    float EnvCollectDtst { get; set; } = 1.0f;

    [SerializeField]
    Transform _indicator;
    [SerializeField]
    Transform _fireSocket;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _speed = 0.5f;

        StartProjectile();

        return true;
    }

    private void Update()
    {
        MovePlayer();
        CollectEnv();
    }

    private void MovePlayer()
    {
        _movePlayer.x = _joystick.Horizontal;
        _movePlayer.y = _joystick.Vertical;

        Vector3 dir = _movePlayer.normalized * _speed * Time.fixedDeltaTime;

        transform.position += dir;

        //_indicator 회전
        if (_movePlayer.normalized != Vector3.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI);
        }

        //좌우 변환
        if (_movePlayer.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_movePlayer.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDtst * EnvCollectDtst;
        List<GemController> gems = Managers.Object.Gems.ToList();
        foreach (GemController gem in gems)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }
        }

        var findGems = GameObject.Find("Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDtst + 0.5f);

        Debug.Log($"SearchGems({findGems.Count}) TotalGems({gems.Count}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if (target == null)
            return;
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        //Debug.Log($"OnDamaged ! {Hp}");

        // TEMP
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }

    //TEMP
    #region FireProjectile
    Coroutine _coFireProjectile;

    void StartProjectile()
    {
        if (_coFireProjectile != null)
            StopCoroutine(_coFireProjectile);

        _coFireProjectile = StartCoroutine(CoStartProjectile());
    }

    IEnumerator CoStartProjectile()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true)
        {
            ProjectileController pc = Managers.Object.Spawn<ProjectileController>(_fireSocket.position, 1);
            pc.SetInfo(1, this, (_fireSocket.position - _indicator.position).normalized);

            yield return wait;
        }
    }

    #endregion
}
