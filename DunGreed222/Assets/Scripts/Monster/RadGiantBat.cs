using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadGiantBat : cLongLangeMonster
{ 

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    Vector2 _Dir;
    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 10;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/BatBullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;
            // _Bullet._Damage = Random.Range(11, 14);
            _Bullet._BulletState = BulletState.Monster;
            //큰박쥐는 총알 잠시 멈춘다
            _Bullet._Start = false;
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }
    }

    void FixedUpdate()
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
    }

    void AnimationEvent()
    {
        _Dir = (Player.GetInstance.transform.position - this.transform.position);

        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
        _Anim.speed = 0;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 10), Mathf.Sin(Mathf.PI * 2 * i / 10));
            dirVec += this.transform.position;
            
            float angle = Mathf.Atan2(-_Dir.x, _Dir.y) * Mathf.Rad2Deg;
            yield return new WaitForSeconds(0.1f);
            FireBulet(dirVec, angle);

        }

        _Anim.speed = 1;
    }
    void FireBulet(Vector3 Dir, float _angle)
    {


        _BulletPoll[_CurBulletIndex].transform.position = Dir;

        _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
    
        _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);



        if (_CurBulletIndex >= _MaxBullet - 1)
        {

            for (int i = 0; i < _MaxBullet; ++i)
            {
                _BulletPoll[i]._Start = true;
                StartCoroutine("ActiveBullet", _BulletPoll[i]);
            }
            _CurBulletIndex = 0;
        }
        else
        {
            _CurBulletIndex++;
        }

    }
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet._Start = false;
        Bullet.gameObject.SetActive(false);
    }
    public override void HIT(int dam)
    {

    }
}
