using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class cShortSward : Shortrange
{
    protected override void Awake()
    {
        base.Awake();
        _ItemID = 1;
        _ItemName = "숏소드";//아이템이름
        _ItemDescrIption = "가볍고 휘두르기 편한 검";//아이템설명
        _Type = ItemType.Sword;//아이템타입
        _MinAttackDamage = 8;//최소데미지
        _MaxAttackDamage = 10;//최대데미지
        _AttackSpeed = 3.03f;//공격속도
        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/BasicShortSword_New");//아이템 이미지
        _ItemPrice = 500;//아이템가격
        _SkillText = "10초간 공격속도 소량 증가";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_SpearWhirl");//아이템 이미지
        _SkillCollTime = 30;
    }
    public override void Attack(cMonsterBase Monster)
    {
        int randomDamage = Random.Range((int)Player.GetInstance._MinDamage, (int)Player.GetInstance._MaxDamage);
        if (Player.GetInstance.isCritical())
        {
            _Dam = (randomDamage - Monster._Defense) + (int)((float)randomDamage * ((float)Player.GetInstance._CriticalDamage / 100.0f))
                + (int)((float)randomDamage * ((float)Player.GetInstance._Power / 100));
            Monster.MonsterHIT(_Dam, true);
        }
        else
        {
            _Dam = (randomDamage - Monster._Defense) + (int)((float)randomDamage * ((float)Player.GetInstance._Power / 100));
            Monster.MonsterHIT(_Dam, false);
        }

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
        Player.GetInstance._Buff.SetTrigger("AttackSpeedBuff");
        Player.GetInstance.SetAttackSpeed(10);
        yield return new WaitForSeconds(10.0f);
        if (!_Skill)
        {
            Player.GetInstance.SetAttackSpeed(-10);
        Player.GetInstance._Buff.SetTrigger("BuffOff");
        }


    }
    public override void StopCorutin()
    {
        StopAllCoroutines();
    }
}
