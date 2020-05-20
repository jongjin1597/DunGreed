using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladius : Shortrange
{
    protected override void Awake()
    {
        base.Awake();
        _ItemID = 5;
        _ItemName = "글라디우스";//아이템이름
        _ItemDescrIption = "밀집된 곳에서 전투를 하기 위해 고안된 검";//아이템설명
        _ItemIcon = Resources.Load<Sprite>("Itemp/Gladius");//아이템이미지
        _Type = ItemType.Sword;//아이템타입
        _MinAttackDamage = 10;//최소데미지
        _MaxAttackDamage = 13;//최대데미지
        _AttackSpeed = 2.5f;//공격속도
        _Quality = ItemQuality.Rare;//아이템등급
        _ItemPrice = 1000;//아이템가격
        
    }

    public override void Skill()
    {

    }
}
