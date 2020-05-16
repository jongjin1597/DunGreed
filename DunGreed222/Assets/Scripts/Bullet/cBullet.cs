using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBullet : MonoBehaviour
{
    public float _Damage;
    public float _Speed;
    public Transform _transform;

    public bool _Start = true;
    private void Awake()
    {
        if (_transform != null)
        {
            _transform.eulerAngles = new Vector3(0f, 0.0f, 0f);
        }
    }
    void Update()
    {
        if (_Start)
        {
            transform.position += transform.up * _Speed * Time.deltaTime;
        }
    }


}
