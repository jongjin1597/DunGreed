using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBossBullet : MonoBehaviour
{
    public int _Damage;
    public float _Speed;
    //회전하는총알
    public Transform _transform;
    public bool _Start = true;

    void Update()
    {
        if (_Start)
        {
            transform.position += transform.up * _Speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int dam = _Damage - Player.GetInstance._Defense;
            Player.GetInstance._health.MyCurrentValue -= dam;
        }

    }
}
