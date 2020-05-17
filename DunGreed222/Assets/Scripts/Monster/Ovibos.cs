using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovibos : cShortMonster
{
    Rigidbody2D _rigid;
    Vector2 dir;

    float speed = 8f;
    bool dash = false;
    float Chack = 0f;

    protected override void Awake()
    {
        base.Awake();

        _rigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Chack += Time.deltaTime;

        if(_rigid.velocity == Vector2.zero)
        {
            _Anim.SetBool("Run", false);
        }

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("OvibosMove"))
        {
            dash = true;
        }
        else
        {
            dash = false;           
        }

        if (!dash)
        {
            if (Player.GetInstance.transform.position.x < this.transform.position.x)
            {
                _Renderer.flipX = true;
            }
            if (Player.GetInstance.transform.position.x > this.transform.position.x)
            {
                _Renderer.flipX = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Chack >= 4f)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _Anim.SetBool("Run", true);
                dir = (Player.GetInstance.transform.position - this.transform.position);
                float dashSpeed = 1.5f;
                if (Player.GetInstance.transform.position.x < this.transform.position.x)
                {
                    dashSpeed *= -1;
                }
                _rigid.velocity = new Vector2((dir.normalized.x * speed) + dashSpeed, _rigid.position.y);
            }
            Chack = 0;
        }
    }

}
