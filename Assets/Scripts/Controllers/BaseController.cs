using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseController : MonoBehaviour
{
    public ObjectType ObjectType { get; protected set; }

    bool _init = false;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    public virtual bool Init()
    {
        if (_init)
            return false;

        _init = true;
        return true;
    }

    private void Update()
    {
        UpdateController();
    }

    public virtual void UpdateController()
    {

    }
}
