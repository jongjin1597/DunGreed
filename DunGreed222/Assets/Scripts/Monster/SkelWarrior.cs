using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelWarrior : cShortMonster
{
    Rigidbody2D _rigid;
    Vector2 dir;
    Vector3 SwordDir;
    public GameObject sword;

    float speed = 2f;
    bool attack = false;
    float Chack = 0f;

    //float attackDelay = 3f; //런 딜레이
    //float attackTimer = 0; //런 타이머
    public float _Radius;
    BoxCollider2D _AttackBox;
    protected override void Awake()
    {
        base.Awake();
        _AttackBox = transform.GetChild(3).GetComponent<BoxCollider2D>();
        _rigid = GetComponent<Rigidbody2D>();
        _MaxHP = 30;
        _currnetHP = 30;
        _Defense = 0;
    }
    private void Update()
    {
        Chack += Time.deltaTime;
        dir = (Player.GetInstance.transform.position - this.transform.position);

        if (!attack)
        {
            _rigid.velocity = new Vector2(dir.normalized.x * speed, _rigid.position.y);           
        }
        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _HPBarBackGround.transform.rotation = Quaternion.identity;
        }
        else   if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            transform.rotation = Quaternion.identity;
            _HPBarBackGround.transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        SwordDir = (Player.GetInstance.transform.position - this.transform.position);
        sword.transform.position = this.transform.position + (SwordDir.normalized * _Radius);

    }
    void Attack()
    {
        if (Chack >= 4f)
        {
            _AttackBox.enabled = true;
            StartCoroutine(BoxEnabled());
                attack = true;
                float swordZ = -120;
                if (sword.GetComponent<SpriteRenderer>().flipX == true)
                {
                    swordZ *= -1;
                }
                sword.transform.Rotate(new Vector3(0, 0, swordZ), Space.Self);
            
            Invoke("swordRotate", 0.5f);
            Chack = 0;
            attack = false;
        }
    }
    IEnumerator BoxEnabled()
    {
        yield return new WaitForSeconds(0.5f);
        _AttackBox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_AttackBox.enabled)
            {
                int Attack = Random.Range(_MinAtteckDamage, _MaxAttackDamage);
                int _dam = Attack - Player.GetInstance._Defense;
                Player.GetInstance.HIT(_dam);
                _AttackBox.enabled = false;
            }


        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
     
            if (other.gameObject.CompareTag("Player"))
            {
            Attack();
            }

    }

    void swordRotate()
    {
        float swordZ = 120;
        if (sword.GetComponent<SpriteRenderer>().flipX == true)
        {
            swordZ *= -1;
        }
        sword.transform.Rotate(new Vector3(0, 0, swordZ), Space.Self);
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);

    }
    public override void DropGold()
    {
        for (int i = 0; i <= 5; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 50 && RandomIndex <= 95)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
            else if (RandomIndex >= 95 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                obj.transform.position = this.transform.position;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
        }
    }
}
