using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCrossBow : Longrange
{

    //float shootDelay = 0.5f;

    protected override void Awake()
    {
        base.Awake();
        _MaxBullet = 20;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/CrossbowArrow")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 5.0f;
            _Bullet._Damage = Random.Range(11,14);
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);

            _BulletPoll.Add(_Bullet);
        }
        _ItemID = 3;
        _ItemName = "손쇠뇌";//아이템이름
        _ItemDescrIption = "활을 고정틀에 올리고 화살을 올려 쏘는 전통 무기";//아이템설명
        _Type = ItemType.Gun;//아이템타입
        _MinAttackDamage = 11;//최소데미지
        _MaxAttackDamage = 13;//최대데미지
        _AttackSpeed = 2.38f;//공격속도
        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Crossbow/Crossbow2");//아이템 이미지
        _ItemPrice = 700;//아이템가격


    }

    public override void Skill()
    { }

    //총알 발사
   public override  void FireBulet(Vector2 Position, float _angle)
    {
            _BulletPoll[curBulletIndex].transform.position = Position;

            _BulletPoll[curBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);

            _BulletPoll[curBulletIndex].gameObject.SetActive(true);
             StartCoroutine("ActiveBullet", _BulletPoll[curBulletIndex]);
            if (curBulletIndex >= _MaxBullet - 1)
            {
                curBulletIndex = 0;
            }
            else
            {
                curBulletIndex++;
            }


    }
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }
}
