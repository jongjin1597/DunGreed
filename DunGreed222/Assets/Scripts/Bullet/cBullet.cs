using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBullet : MonoBehaviour
{
    public int _Damage;
    public float _Speed;
    //회전하는총알
    public Transform _transform;
    //이총알이 플레이어용인지 몬스터용인지
    public bool _Player=true;
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

        if (_Player) 
        {
            if (collision.CompareTag("Monster"))
            {
        
            }
        }
        else if (!_Player)
        {
            if (collision.CompareTag("Player"))
            {
               int dam =_Damage - Player.GetInstance._Defense;
                Player.GetInstance._health.MyCurrentValue -= dam;
            }
        }
        
    }
}
