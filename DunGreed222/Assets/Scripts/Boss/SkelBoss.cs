using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelBoss : cLongLangeMonster
{
    public float shootDelay = 4f; //총알 딜레이
    float shootTimer = 0; //총알 타이머
    public Transform BossBack;

    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 50;
        for (int i = 0; i < _MaxBullet; ++i)
        {
            GameObject obj = Instantiate(Resources.Load("Prefabs/Boss/BossBullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;
            //_Bullet._Damage = Random.Range(11, 14);
            _Bullet._Player = false;
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.transform.SetParent(BossBack);
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }
        Debug.Log("씨발년아");
    }   

    public void AnimationEvent()
    {
       
        for (int i = 0; i < _MaxBullet; ++i)
        {
           
            Vector3 dirVec = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 12), Mathf.Sin(Mathf.PI * 2 * i / 12));
            dirVec += this.transform.position;
            float angle = 30 * i;
            FireBulet(dirVec, angle);
        }
    }
    public void FireBulet(Vector3 Dir, float _angle)
    {
        Debug.Log("씨발년아");
        //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }


        _BulletPoll[_CurBulletIndex].transform.position = Dir;

        _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _BulletPoll[_CurBulletIndex]._transform.rotation = Quaternion.Euler(0f, 0f, 0);
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
}
