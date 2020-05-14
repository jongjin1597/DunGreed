using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ovibos : cCharacter
{
    Rigidbody2D _rigid;
    Vector2 dir;

    float speed = 6f;

    protected override void Awake()
    {
        base.Awake();

        _rigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
            //attackSpeed *= 1;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
            //attackSpeed *= -1;
        }
       

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _Anim.SetBool("Run", true);
            dir = (Player.GetInstance.transform.position - this.transform.position);
            _rigid.velocity = new Vector2(dir.normalized.x * speed, _rigid.position.y);
        }
    }

}
