using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Berdysh : Shortrange
{
    protected override void Awake()
    {
        base.Awake();
        _ItemID = 6;
        _ItemName = "버디슈";//아이템이름
        _ItemDescrIption = "초승달 모양의 날이 선 무기";//아이템설명
        _Type = ItemType.Spear;//아이템타입
        _MinAttackDamage = 18;//최소데미지
        _MaxAttackDamage = 22;//최대데미지
        _AttackSpeed = 0.95f;//공격속도
        _Quality = ItemQuality.Rare;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Berdysh");//아이템 이미지
        _ItemPrice = 1000;//아이템가격
        _SkillText = "15초 동안 공격속도 증가";
        _SkillIcon = Resources.Load<Sprite>("Skill/Skill_SpearWhirl");
        _SkillCollTime = 20.0f;
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
        Player.GetInstance.SetAttackSpeed(25);

        yield return new WaitForSeconds(15.0f);
        if (!_Skill)
        {
            Player.GetInstance.SetAttackSpeed(-25);
        Player.GetInstance._Buff.SetTrigger("BuffOff");
        }
    }
    public override void StopCorutin() 
    {
        StopAllCoroutines();
    }
}
