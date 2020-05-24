using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBow : cShortMonster
{


    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머


    public Transform Skel;
    public float _Radius;

    protected override void Awake()
    {
        base.Awake();
        Skel = transform.GetChild(1);
        _Renderer = GetComponent<SpriteRenderer>();
        _MaxHP = 30;
        _currnetHP = 30;
        _Defense = 0;

    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > shootDelay) //쿨타임이 지났는지
        {
            _Anim.SetTrigger("Fire");
            shootTimer = 0; //쿨타임 초기화
        }

        if (Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }


        Vector3 dir = (Player.GetInstance.transform.position - this.transform.position);
        Skel.transform.position = this.transform.position + (dir.normalized * _Radius);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Skel.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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