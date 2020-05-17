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

    float attackDelay = 3f; //런 딜레이
    float attackTimer = 0; //런 타이머
    public float _Radius;

    protected override void Awake()
    {
        base.Awake();

        _rigid = GetComponent<Rigidbody2D>();
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
            _Renderer.flipX = true;
            sword.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
            sword.GetComponent<SpriteRenderer>().flipX = false;
        }


        SwordDir = (Player.GetInstance.transform.position - this.transform.position);
        sword.transform.position = this.transform.position + (SwordDir.normalized * _Radius);

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (Chack >= 4f)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                attack = true;
                float swordZ = -120;
                if (sword.GetComponent<SpriteRenderer>().flipX == true)
                {
                    swordZ *= -1;                 
                }
                sword.transform.Rotate(new Vector3(0, 0, swordZ), Space.Self);
            }
            Invoke("swordRotate", 0.5f);
            Chack = 0;
            attack = false;
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
}
