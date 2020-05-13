using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private const float bulletSpeed = 5f; // 총알속도
    float DestroyTime;
    public Transform _transform;
    private void Awake()
    {
        DestroyTime = 2.0f;
        Destroy(this.gameObject, DestroyTime);
        if (_transform != null)
        {
            _transform.eulerAngles = new Vector3(0f, 0.0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    

}
