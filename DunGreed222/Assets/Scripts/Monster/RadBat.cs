using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadBat : cLongLangeMonster
{ 
    Rigidbody2D _rigid;
    public int _moveRangeX;
    public int _moveRangeY;

    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    protected override void Awake()
    {
        base.Awake();
        _MaxBullet =1;

            GameObject Obj = Instantiate(Resources.Load("Prefabs/Bullet/BabyBatBullet")) as GameObject;
            cBullet _bullet = Obj.gameObject.GetComponent<cBullet>();
            _bullet._Speed = 5.0f;
            _bullet._Damage = 3;
            _bullet._BulletState = BulletState.Monster;
            _bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            Obj.gameObject.SetActive(false);
            _BulletPoll.Add(_bullet);

        _rigid = GetComponent<Rigidbody2D>();
        Invoke("MoveRange", 1);
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
        

        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("Bat_Rad"))
        {
            _rigid.velocity = new Vector2(_moveRangeX, _moveRangeY);
        }
        else if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("Bat_RadAttack"))
        {
            _rigid.velocity = Vector2.zero;
        }

        if(Player.GetInstance.transform.position.x < this.transform.position.x)
        {
            _Renderer.flipX = true;
        }
        if (Player.GetInstance.transform.position.x > this.transform.position.x)
        {
            _Renderer.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "floor" || other.gameObject.tag == "BaseLine")
        {
            _moveRangeX *= -1;
            _moveRangeY *= -1;
            CancelInvoke();
            Invoke("MoveRange", 0.1f);
        }
    }

    //재귀 함수
    void MoveRange()
    {
        _moveRangeX = Random.Range(-2, 3);
        _moveRangeY = Random.Range(-2, 3);

        float naxtMoveRange = Random.Range(2f, 4f);
        Invoke("MoveRange", naxtMoveRange);
    }

    public void AnimationEvent()
    {
     
            Vector2 dir = (Player.GetInstance.transform.position - this.transform.position);
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            FireBulet( angle);
    }
    public  void FireBulet( float _angle)
    {

        ////발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }


            _BulletPoll[_CurBulletIndex].transform.position = this.transform.position;

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
        for (int i = 0; i <= 10; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if (RandomIndex >= 55 && RandomIndex <= 100)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                GoldX = Random.Range(-100, 100);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            }
         
        }
    }
}
