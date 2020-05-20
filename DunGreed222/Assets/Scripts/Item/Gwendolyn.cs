using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gwendolyn : Shortrange
{
    protected override void Awake()
    {
     
        _ItemID = 10;
        _ItemName = "그웬돌린";//아이템이름
        _ItemDescrIption = "마력을 흡수하는 푸른 돌을 깍아 만든 창";//아이템설명
        _Type = ItemType.Spear;//아이템타입
        _MinAttackDamage = 16;//최소데미지
        _MaxAttackDamage = 22;//최대데미지
        _AttackSpeed = 2f;//공격속도
        _Quality = ItemQuality.Unique;//아이템등급    
        _ItemIcon = Resources.Load<Sprite>("Itemp/Gwendolyn");//아이템 이미지
        _ItemPrice = 2000;//아이템가격
        _SkillText = "30초간 크리티컬 데미지 증가";
        _SkillIcon = Resources.Load<Sprite>("UI/Stat/CritcalDamage");
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
            StartCoroutine("SkillCorutin");

        }
    }
    IEnumerable SkillCorutin()
    {
        _Skill = false;
        Player.GetInstance._CriticalDamage += 30;
        yield return new WaitForSeconds(30.0f);

        Player.GetInstance._CriticalDamage -= 30;
        _Skill = true;
    }
}
