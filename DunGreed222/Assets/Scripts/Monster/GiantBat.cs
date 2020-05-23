using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBat : cLongLangeMonster
{

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    Vector2 dir;

   protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 12;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject Obj = Instantiate(Resources.Load("Prefabs/Bullet/BatBullet")) as GameObject;
            cBullet _Bullet = Obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;
            _Bullet._BulletState = BulletState.Monster;
            //_Bullet._Damage = Random.Range(11, 14);
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }
    }

    // Update is called once per frame
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

    public void AnimationEvent()
    {
        dir = (Player.GetInstance.transform.position - this.transform.position);
        for (float i = 0f; i <= 0.8f; i += 0.4f)
        {
            Invoke("Attack", i);
        }
    }

    public void Attack()
    {
        for (int i = -1; i < 2; ++i)
        {
           
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            angle += 25 * i;
            FireBulet(this.transform.position, angle);
        }
    }
    public void FireBulet(Vector3 Dir, float _angle)
    {

        //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }


        _BulletPoll[_CurBulletIndex].transform.position = Dir;

        _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _BulletPoll[_CurBulletIndex]._Start = true;
        _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
        StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);
        if (_CurBulletIndex >= _MaxBullet - 1)
        {
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
        Bullet.gameObject.SetActive(false);
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
            if (RandomIndex >= 35 && RandomIndex <= 80)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
            else if (RandomIndex >= 80 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
        }
    }

}
