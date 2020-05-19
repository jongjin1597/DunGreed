using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBow : cLongLangeMonster
{


    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머


    public Transform Skel;
    public float _Radius;

    protected override void Awake()
    {
        base.Awake();

        _Renderer = transform.parent.GetComponent<SpriteRenderer>();

        GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Arrow")) as GameObject;
        cBullet _Bullet = obj.GetComponent<cBullet>();
        _Bullet._Speed = 5.0f;
        _Bullet._BulletState = BulletState.Monster;
        _Bullet._Damage =5;
        _Bullet.transform.SetParent(transform.parent);
        //총알 발사하기 전까지는 비활성화 해준다.
        _Bullet.gameObject.SetActive(false);

        _BulletPoll.Add(_Bullet);

 

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


        Vector3 dir = (Player.GetInstance.transform.position - Skel.transform.position);
        this.transform.position = Skel.transform.position + (dir.normalized * _Radius);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    public void AnimationEvent()
    {
        Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        FireBulet(angle);
    }



    public void FireBulet(float _angle)
    {

        //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }
        _BulletPoll[_CurBulletIndex].transform.position = transform.position;

        _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _BulletPoll[_CurBulletIndex]._Start = true;

        _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
        StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);

    }

    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }
    public override void HIT(int dam)
    {

    }
}