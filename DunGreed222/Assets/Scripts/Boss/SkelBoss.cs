using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBoss : cBossMonster
{
    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    public Transform BossBack;
    int SwordX = 0;
    
    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 120;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossBullet")) as GameObject;
            cBossBullet _Bullet = obj.GetComponent<cBossBullet>();
            _Bullet._Speed = 5.0f;
            //_Bullet._Damage = Random.Range(11, 14);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.transform.SetParent(transform);
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
       
        Vector3 dirVec = new Vector3(BossBack.transform.position.x + -4, BossBack.transform.position.y + 5, 0);
       
        StartCoroutine(FireSword(dirVec));
    }


    private void Update()
    {
       
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
        //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }

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

       

    }
    
    IEnumerator ActiveBullet(cBossBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }

    IEnumerator ActiveSword(cBossSword Sword)
    {
        yield return new WaitForSeconds(3.0f);
        Sword.gameObject.SetActive(false);
    }
}
