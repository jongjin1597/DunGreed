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
    BoxCollider2D _AttackBox;
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(2).GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _MaxHP = 70;
        _currnetHP = 70;
        _Defense = 1;
        _AttackDamage = 10;
    }


   void FixedUpdate()
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
        if (_currnetHP < 1)
        {
            if (!_isDie)
            {
                Die(this.gameObject);
                _isDie = true;
            }
        }
    }
    void Attack()
    {
        if (Chack >= 4f)
        {
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
            _Anim.SetBool("Run", true);
            dir = (Player.GetInstance.transform.position - this.transform.position);
            float dashSpeed = 1.5f;
            if (Player.GetInstance.transform.position.x < this.transform.position.x)
            {
                dashSpeed *= -1;
            }
            _rigid.velocity = new Vector2((dir.normalized.x * speed) + dashSpeed, 0);
            Chack = 0;
        }

    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(1f);
        _AttackBox.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     
            if (other.gameObject.CompareTag("Player"))
            {
            if (_AttackBox.enabled)
             {

                int _dam = _AttackDamage - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
            }
            }
     }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);

    }
    public override void DropGold()
    {
        for (int i = 0; i <= 7; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 30 && RandomIndex <= 75)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
            else if (RandomIndex >= 75 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
        }
    }
}
