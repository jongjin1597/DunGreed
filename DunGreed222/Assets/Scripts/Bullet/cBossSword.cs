using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBossSword : MonoBehaviour
{
    public int _Damage;
    public float _Speed;
    //회전하는총알
    public Transform _transform;
    public bool _Start = true;
    float shootDelay = 0;
    float _angle;

    void Update()
    {
        shootDelay += Time.deltaTime;
        if (_Start)
        {
            if (shootDelay < 1)
            {
                Vector2 _Dir = (Player.GetInstance.transform.position - this.transform.position);
                _angle = Mathf.Atan2(-_Dir.x, _Dir.y) * Mathf.Rad2Deg;

            }
            else if (shootDelay >= 1)
            {
                transform.position += transform.up * _Speed * Time.deltaTime;
            }
        }
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int dam = _Damage - Player.GetInstance._Defense;
            Player.GetInstance._health.MyCurrentValue -= dam;
        }

       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("floor"))
        {
            _Start = false;

        }
    }
}
