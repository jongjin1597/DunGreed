using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kar98 : Longrange
{

    //float shootDelay = 0.5f;

    private void Awake()
    {

        _MaxBullet = 5;
        for (int i = 0; i < _MaxBullet; ++i)
        {
           GameObject obj = Instantiate(Resources.Load("Prefabs/Bullet/Bullet")) as GameObject;
            cBullet _Bullet = obj.GetComponent<cBullet>();
            _Bullet._Speed = 40.0f;
            _Bullet._Damage = Random.Range(27,35);
            _Bullet.transform.SetParent(transform);
            //총알 발사하기 전까지는 비활성화 해준다.
            _Bullet.gameObject.SetActive(false);
            _Bullet._BulletState = BulletState.Player;
            _BulletPoll.Add(_Bullet);
        }
        _Delay = 0.5f;
        _ItemID = 9;
        _ItemName = "1898년형 단기병총";//아이템이름
        _ItemDescrIption = "이 나라의 기술력은 세계 제일이지! -피아트-";//아이템설명
        _Type = ItemType.OneShot;//아이템타입
        _MinAttackDamage = 27;//최소데미지
        _MaxAttackDamage = 35;//최대데미지
        _AttackSpeed = 0.87f;//공격속도

        _Quality = ItemQuality.Unique;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Crossbow/Kar98");//아이템 이미지
        _ItemPrice = 3000;//아이템가격
        _SkillText = "다음 한발의 총알의 공격력이 아주강해집니다.";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_DeadlyShot");//아이템 이미지
        _SkillCollTime = 30;
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
        cBullet Bullet= _BulletPoll[_CurBulletIndex];
        Bullet._Damage += 30;
         yield return new WaitForSeconds(30.0f);
        if (!_Skill)
        {
            Bullet._Damage -= 30;

        Player.GetInstance._Buff.SetTrigger("BuffOff");
        }
    }

}
