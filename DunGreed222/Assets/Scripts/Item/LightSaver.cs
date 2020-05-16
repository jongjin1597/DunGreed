using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaver : Shortrange
{
    protected override void Awake()
    {
        base.Awake();
        _ItemID = 9;
        _ItemName = "라이트 세이버";//아이템이름
        _ItemDescrIption = "고온의 플라즈마를 칼날삼아 모든 걸 베어버리는 광검";//아이템설명
        _ItemIcon = Resources.Load<Sprite>("Itemp/LightSaber");//
        _Type = ItemType.Sword;//아이템타입
        _MinAttackDamage = 12;//최소데미지
        _MaxAttackDamage = 12f;//최대데미지
        _AttackSpeed = 3.28f;//공격속도
        _Quality = ItemQuality.Unique;//아이템등급
        _ItemPrice = 2000;//아이템가격

    }
    public override void Skill()
    {

    }
}
