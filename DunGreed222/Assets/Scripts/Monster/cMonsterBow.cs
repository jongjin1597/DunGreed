using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMonsterBow : cLongLangeMonster
{

    protected override void Awake()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Arrow")) as GameObject;
        cBullet _Bullet = obj.GetComponent<cBullet>();
        _Bullet._Speed = 5.0f;
        _Bullet._BulletState = BulletState.Monster;
        _Bullet._Damage = 5;
        _Bullet.transform.SetParent(transform.parent);
        //총알 발사하기 전까지는 비활성화 해준다.
        _Bullet.gameObject.SetActive(false);

        _BulletPoll.Add(_Bullet);


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
}
