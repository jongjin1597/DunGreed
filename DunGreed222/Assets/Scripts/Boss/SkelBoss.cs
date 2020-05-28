using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkelBoss : cBossMonster
{
    enum State
    {
        Normal,
        Bullet,
        Sword,
        Laser
    }
    bool _isDie=false;
    public Transform BossBack;
    int SwordX = 0;
    bool isAttack=false;
    int _RandomIndex;
    State state = State.Normal;
    //public List<SkellBossLaser> skellBossLasers =new List<SkellBossLaser>();
    SkellBossLaser[] skellBossLasers =new SkellBossLaser[2];

    protected override void Awake()
    {
        base.Awake();

        _MaxBossSword = 5;
        for (int i = 0; i < _MaxBossSword; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossSword")) as GameObject;
            cBossSword _Sword = obj.GetComponent<cBossSword>();
            _Sword._Speed = 20.0f;
  
            _Sword.transform.SetParent(transform);
            _Sword.gameObject.SetActive(false);

            _BossSwordPoll.Add(_Sword);
        }
        skellBossLasers[0] = transform.GetChild(2).GetComponent<SkellBossLaser>();
        skellBossLasers[1] = transform.GetChild(3).GetComponent<SkellBossLaser>();
        skellBossLasers[0]._SkellLaser = skellBossLasers[1];
        skellBossLasers[0].count = 0;
        skellBossLasers[1]._SkellLaser = skellBossLasers[0];
        skellBossLasers[1].count = 0;
        _MaxHP = 400;
        _currnetHP = 400;
    }

    private void Start()
    {
        StartCoroutine("SkellBossState");
    }


    private void FixedUpdate()
    {
        if (!isAttack)
        {
            if (state == State.Laser)
            {
                _RandomIndex = Random.Range(0, 2);
                skellBossLasers[_RandomIndex].Fire = true;
                isAttack = true;
               
            }
            else if (state == State.Bullet)
            {
                _Anim.SetTrigger("Attack");
                isAttack = true;
              
            }
            else if (state == State.Sword)
            {
                Vector3 dirVec = new Vector3(BossBack.transform.position.x + -4, BossBack.transform.position.y + 5, 0);
                StartCoroutine(FireSword(dirVec));
                isAttack = true;
           
            }
        }
   

        if (skellBossLasers[0].count > 3|| skellBossLasers[1].count >3)
        {
            if(state!=State.Normal)
            StartCoroutine("SkellBossState");
            state = State.Normal;
        }
        else if (_currnetHP <= 0)
        {
            if (!_isDie)
            {
                Die(this.gameObject);
                _isDie = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            _currnetHP -= 10;
        }
    }

    IEnumerator SkellBossState()
    {
        yield return new WaitForSeconds(3.0f);

        int randomNum = 2;
        if(randomNum == 0)
        {
            state = State.Bullet;
        }
        else if (randomNum == 1)
        {
            state = State.Sword;
        }
        else if (randomNum == 2)
        {
            state = State.Laser;
            skellBossLasers[0].count=0;
            skellBossLasers[1].count = 0;


        }
        isAttack = false;
    }

    IEnumerator AnimationEvent()
    {
        float _BulletAngle = 0;
        for (int j = 0; j < 30; ++j)
        {
            for (int i = 0; i < 4; ++i)
            {
                Vector3 dirVec = BossBack.transform.position;

                float angle = 90 * i+ _BulletAngle;
                FireBulet(dirVec, angle);
            }
            yield return new WaitForSeconds(0.2f);
            _BulletAngle += 5;
        }
        state = State.Normal;
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    }
    public void FireBulet(Vector3 Dir, float _angle)
    {


        cBullet Bullet = cMonsterBullet.GetInstance.GetObject(0);
        Bullet.transform.position = Dir;
        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        Bullet._Start = true;
        Bullet._ChildBullet.rotation = Quaternion.Euler(0f, 0f, 0);
        Bullet.gameObject.SetActive(true);


        StartCoroutine(ActiveBullet(Bullet));
    }

    IEnumerator FireSword(Vector3 Dir)
    {
        _BossSwordPoll[_CurBossSwordIndex].transform.position = Dir;       
        _BossSwordPoll[_CurBossSwordIndex].gameObject.SetActive(true);
        _BossSwordPoll[_CurBossSwordIndex]._Start = true;

        yield return new WaitForSeconds(0.2f);
        //StartCoroutine("ActiveSword", _BossSwordPoll[_CurBossSwordIndex]);
        if (_CurBossSwordIndex >= _MaxBossSword - 1)
        {
            SwordX = 0;
            _CurBossSwordIndex = 0;
        }
        else
        {
            SwordX += 2;
            Vector3 dirVec = new Vector3(BossBack.transform.position.x + -4 + SwordX, BossBack.transform.position.y + 5, 0);
            _CurBossSwordIndex++;
            StartCoroutine(FireSword(dirVec));           
        }

        if (_BossSwordPoll[_CurBossSwordIndex] == _BossSwordPoll[4])
        {
            yield return new WaitForSeconds(2.0f);
            StartCoroutine("SkellBossState");
            state = State.Normal;
        }
    }
    
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet._Start = false;
        Bullet.gameObject.SetActive(false);
        
    }


}
