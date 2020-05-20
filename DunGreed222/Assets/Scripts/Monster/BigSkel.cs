using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSkel : cShortMonster
{
    Rigidbody2D _rigid;
    Vector2 dir;

    float runDelay = 2f; //런 딜레이
    float runTimer = 0; //런 타이머
    float speed = 2f;

    float Chack = 0f;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        _rigid = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();
        _Renderer = GetComponentInChildren<SpriteRenderer>();
        //Footbox = gameObject.GetComponentInChildren<GameObject>();
        _Anim.SetBool("Run", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Chack += Time.deltaTime;
        dir = (Player.GetInstance.transform.position - this.transform.position);

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("BigWhiteSkelMove"))
        {
            _rigid.velocity = new Vector2(dir.normalized.x * speed, _rigid.position.y);
        }
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("BigWhiteSkelAttack"))
        {
            _rigid.velocity = Vector2.zero;
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }


    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (Chack >= 4f)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _Anim.SetTrigger("Attack");                
            }
            Chack = 0;
        }
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        GameObject Dam = Instantiate(_Damage);
        Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Dam.GetComponent<cDamageText>().SetDamage(dam, isCritical);
        if (_currnetHP > 0)
        {
            _currnetHP -= dam;
        }
        else if (_currnetHP <= 0)
        {
            Destroy(this);
        }
    }
}
