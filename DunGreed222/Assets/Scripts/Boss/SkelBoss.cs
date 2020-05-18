using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBoss : cLongLangeMonster
{
    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    public Transform BossBack;
    private float _RotateZ;

    float _BulletAngle;
    protected override void Awake()
    {
        base.Awake();
        _MaxBullet =120;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossBullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;
            //_Bullet._Damage = Random.Range(11, 14);
            _Bullet._Player = false;
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.transform.SetParent(transform);
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }
    }   

  IEnumerator AnimationEvent()
    {
        _Anim.speed = 0;
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
        _Anim.speed = 1;
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

    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.transform.parent = BossBack;

    }
}
