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

    public Transform BossBack;
    int SwordX = 0;
    int laserCount = 0;

    State state = State.Normal;
    public SkellBossLaser[] skellBossLasers;


    protected override void Awake()
    {
        base.Awake();

        _MaxBullet = 120;

        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossBullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;

            _Bullet._BulletState = BulletState.Boss;

            _Bullet.transform.SetParent(BossBack);
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }

        _MaxBossSword = 5;
        for (int i = 0; i < _MaxBossSword; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossSword")) as GameObject;
            cBossSword _Sword = obj.GetComponent<cBossSword>();
            _Sword._Speed = 20.0f;
            //_Bullet._Damage = Random.Range(11, 14);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Sword.transform.SetParent(transform);
            _Sword.gameObject.SetActive(false);

            _BossSwordPoll.Add(_Sword);
        }

    }

    private void Start()
    {
        StartCoroutine("SkellBossState");
    }


    private void Update()
    {

        if (state == State.Laser)
        {
            skellBossLasers[0].Fire = true;
            laserCount = 0;
            state = State.Normal;
        }
        else if (state == State.Bullet)
        {
            _Anim.SetTrigger("Attack");
            state = State.Normal;
        }
        else if (state == State.Sword)
        {
            Vector3 dirVec = new Vector3(BossBack.transform.position.x + -4, BossBack.transform.position.y + 5, 0);
            StartCoroutine(FireSword(dirVec));
            state = State.Normal;
        }
 
        if (laserCount < 2)
        {
            if (skellBossLasers[0]._anim[2].GetCurrentAnimatorStateInfo(0).IsName("SkellBossLaserBody") &&
               skellBossLasers[0]._anim[2].GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
            {
                skellBossLasers[1].Fire = true;
                laserCount++;
            }
            else if (skellBossLasers[1]._anim[2].GetCurrentAnimatorStateInfo(0).IsName("SkellBossLaserBody") &&
               skellBossLasers[1]._anim[2].GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
            {
                skellBossLasers[2].Fire = true;
                laserCount++;
                StartCoroutine("SkellBossState");
            }
        }

        //if (!_BossSwordPoll[4].gameObject.activeSelf){
        //    StartCoroutine("SkellBossState");
        //}
    }

    IEnumerator SkellBossState()
    {
        yield return new WaitForSeconds(2.0f);
        int randomNum = 2;

        //int randomNum = Random.Range(0, 3);
        if (randomNum == 0)
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
        }
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
     }
    
    public void FireBulet(Vector3 Dir, float _angle)
    {

        _BulletPoll[_CurBulletIndex].transform.position = Dir;

        _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);

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
        }
    }
    
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }

}