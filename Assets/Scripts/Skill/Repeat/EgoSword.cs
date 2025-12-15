using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EgoSword : RepeatSkill
{
    [SerializeField]
    ParticleSystem[] _swingParticles;

    protected enum SwingType
    {
        First,
        Second,
        Third,
        Fourth
    }

    public EgoSword()
    {

    }

    protected override IEnumerator CoStartSkill()
    {
        WaitForSeconds wait = new WaitForSeconds(CoolTime);

        while (true)
        {
            SetParticles(SwingType.First);
            _swingParticles[(int)SwingType.First].gameObject.SetActive(true);
            yield return new WaitForSeconds(_swingParticles[(int)SwingType.First].main.duration);

            SetParticles(SwingType.Second);
            _swingParticles[(int)SwingType.Second].gameObject.SetActive(true);
            yield return new WaitForSeconds(_swingParticles[(int)SwingType.Second].main.duration);

            SetParticles(SwingType.Third);
            _swingParticles[(int)SwingType.Third].gameObject.SetActive(true);
            yield return new WaitForSeconds(_swingParticles[(int)SwingType.Third].main.duration);

            SetParticles(SwingType.Fourth);
            _swingParticles[(int)SwingType.Fourth].gameObject.SetActive(true);
            yield return new WaitForSeconds(_swingParticles[(int)SwingType.Fourth].main.duration);

            yield return wait;
        }
    }

    public override bool Init()
    {
        base.Init();

        return true;
    }

    void SetParticles(SwingType swingType)
    {
        if (Managers.Game.Player == null)
            return;

        Vector3 tempAngle = Managers.Game.Player.Indicator.transform.eulerAngles;
        transform.localEulerAngles = tempAngle;
        transform.position = Managers.Game.Player.transform.position;

        float radian = Mathf.Deg2Rad * tempAngle.z * -1;

        var main = _swingParticles[(int)swingType].main;
        main.startRotation = radian;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.transform.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;

        mc.OnDamaged(Owner, Damage);
    }

    protected override void DoSkillJob()
    {

    }



    #region EgoSwordController.cs
    //[SerializeField]
    //ParticleSystem[] _swingParticles;

    //protected enum SwingType
    //{
    //    First,
    //    Second,
    //    Third,
    //    Fourth
    //}

    //public override bool Init()
    //{
    //    base.Init();

    //    // Active 될때까지 콜라이더 물리적용 X
    //    for (int i = 0; i < _swingParticles.Length; i++)
    //        _swingParticles[i].GetComponent<Rigidbody2D>().simulated = false;

    //    for (int i = 0; i < _swingParticles.Length; i++)
    //        _swingParticles[i].gameObject.GetOrAddComponent<EgoSwordChild>().SetInfo(Managers.Object.Player, 100);

    //    return true;
    //}

    //public void ActivateSkill()
    //{
    //    StartCoroutine(CoSwingSword());
    //}

    //float CoolTime = 2.0f;

    //IEnumerator CoSwingSword()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(CoolTime);

    //        SetParticles(SwingType.First);
    //        _swingParticles[(int)SwingType.First].Play();
    //        TurnOnPhysics(SwingType.First, true);
    //        yield return new WaitForSeconds(_swingParticles[(int)SwingType.First].main.duration);
    //        TurnOnPhysics(SwingType.First, false);

    //        SetParticles(SwingType.Second);
    //        _swingParticles[(int)SwingType.Second].Play();
    //        TurnOnPhysics(SwingType.Second, true);
    //        yield return new WaitForSeconds(_swingParticles[(int)SwingType.Second].main.duration);
    //        TurnOnPhysics(SwingType.Second, false);

    //        SetParticles(SwingType.Third);
    //        _swingParticles[(int)SwingType.Third].Play();
    //        TurnOnPhysics(SwingType.Third, true);
    //        yield return new WaitForSeconds(_swingParticles[(int)SwingType.Third].main.duration);
    //        TurnOnPhysics(SwingType.Third, false);

    //        SetParticles(SwingType.Fourth);
    //        _swingParticles[(int)SwingType.Fourth].Play();
    //        TurnOnPhysics(SwingType.Fourth, true);
    //        yield return new WaitForSeconds(_swingParticles[(int)SwingType.Fourth].main.duration);
    //        TurnOnPhysics(SwingType.Fourth, false);
    //    }
    //}

    ////스킬 각도 조정
    //void SetParticles(SwingType swingType)
    //{
    //    float z = transform.parent.transform.eulerAngles.z;
    //    float radian = (Mathf.PI / 180) * z * -1;

    //    var main = _swingParticles[(int)swingType].main;
    //    main.startRotation = radian;
    //}

    ////스킬 물리 적용 유무
    //private void TurnOnPhysics(SwingType swingType, bool simulated)
    //{
    //    for (int i = 0; i < _swingParticles.Length; i++)
    //        _swingParticles[i].GetComponent<Rigidbody2D>().simulated = false;

    //    _swingParticles[(int)swingType].GetComponent<Rigidbody2D>().simulated = simulated;
    //}

    #endregion

    #region EgoSwordChild.cs
    //using System.Collections;
    //using System.Collections.Generic;
    //using UnityEngine;

    //public class EgoSwordChild : MonoBehaviour
    //{
    //    BaseController _owner;
    //    int _damage;

    //    public void SetInfo(BaseController owner, int damage)
    //    {
    //        _owner = owner;
    //        _damage = damage;
    //    }

    //    private void OnTriggerEnter2D(Collider2D collision)
    //    {
    //        MonsterController mc = collision.transform.GetComponent<MonsterController>();
    //        if (mc.IsValid() == false)
    //            return;

    //        mc.OnDamaged(_owner, _damage);
    //    }
    //}
    #endregion
}
