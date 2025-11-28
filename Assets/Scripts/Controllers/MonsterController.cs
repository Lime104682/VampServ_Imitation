using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    // Start is called before the first frame update
    public override bool Init()
    {
        if (base.Init())
            return false;

        ObjectType = Define.ObjectType.Monster;

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
