using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cMT8 : Longrange
{

    //float shootDelay = 0.5f;

    private void Awake()
    {

        _MaxBullet = 30;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Bullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 30.0f;
            _Bullet._Damage = Random.Range(3,5);
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);
            _Bullet._BulletState = BulletState.Player;
            _BulletPoll.Add(_Bullet);
        }
        _Delay = 0.03f;
        _ItemID = 6;
        _ItemName = "MT8 카빈";//아이템이름
        _ItemDescrIption = "가볍고 연사가 빠른 돌격소총";//아이템설명
        _Type = ItemType.Gun;//아이템타입
        _MinAttackDamage = 3;//최소데미지
        _MaxAttackDamage = 5;//최대데미지
        _AttackSpeed = 10.53f;//공격속도
        _Quality = ItemQuality.Rare;//아이템등급    

        _ItemIcon = Resources.Load<Sprite>("Itemp/Rifle0");//아이템 이미지
        _ItemPrice = 1500;//아이템가격
        _SkillText = "10초간 위력 대폭증가";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_WillpowerOfGladiator");//아이템 이미지
        _SkillCollTime = 20;
    }

    public override void Skill()
    {
        if (_Skill)
        {
            StartCoroutine(SkillCourutin());
        }
    }
    IEnumerator SkillCourutin()
    {
        _Skill = false;
        Player.GetInstance._Buff.SetTrigger("PowerBuff");
        Player.GetInstance._Power += 30;
        yield return new WaitForSeconds(10.0f);
        if (!_Skill)
        {

            Player.GetInstance._Power -= 30;
        Player.GetInstance._Buff.SetTrigger("BuffOff");
        }

    }
    //총알 발사
    public override  void FireBulet(Vector2 Position, float _angle)
    {
        base.FireBulet(Position, _angle);

    }
    IEnumerator ActiveBullet(cBullet Bullet)
    {
        yield return new WaitForSeconds(3.0f);
        Bullet.gameObject.SetActive(false);
    }

}
