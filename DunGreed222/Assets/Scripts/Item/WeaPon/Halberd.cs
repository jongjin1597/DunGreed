using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Halberd : Shortrange
{
    protected override void Awake()
    {
        base.Awake();
        _ItemID =2;
        _ItemName = "미늘창";//아이템이름
        _ItemDescrIption = "도끼처럼 혹은 창처럼 사용할 수 있는 무기";//아이템설명
        _Type = ItemType.Spear;//아이템타입
        _MinAttackDamage = 14;//최소데미지
        _MaxAttackDamage = 20;//최대데미지
        _AttackSpeed = 1.43f;//공격속도
        _Quality = ItemQuality.Normal;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Halberd");//아이템 이미지
        _ItemPrice = 500;//아이템가격
        _SkillText = "5초간 공격속도 대폭증가";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_SpearWhirl");//아이템 이미지
        _SkillCollTime = 20;

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
        Player.GetInstance.SetAttackSpeed(50);
        yield return new WaitForSeconds(5.0f);
        if (!_Skill)
        {
            Player.GetInstance.SetAttackSpeed(-50);

        }

        Player.GetInstance._Buff.SetTrigger("BuffOff");

    }
    public override void StopCorutin()
    {
        StopAllCoroutines();
    }
}
