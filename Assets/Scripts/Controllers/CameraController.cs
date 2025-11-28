using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject _target;

    Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position - _target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _position + _target.transform.position;
    }
}
