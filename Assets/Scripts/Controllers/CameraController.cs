using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//카메라 이동

public class CameraController : MonoBehaviour
{
    public GameObject _target;

    // Update is called once per frame
    void Update()
    {
        if(_target == null)
            return;

        transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, -10);
    }
}
