﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Gun,
    Sword,
    Spear
}
public enum ItemQuality
{
    Normal,
    Rare,
    Unique
}
[System.Serializable]
public class Item
{                                      
    public int _ItemID                               ;              //아이템번호
    public string _ItemName                          ;              //아이템이름
    public string _ItemDescrIption                   ;              //아이템설명
    public Sprite _ItemIcon                          ;              //아이템이미지
    public  ItemType _Type                           ;              //아이템타입
    public float _MinAttackDamage                    ;              //공격최소데미지
    public float _MaxAttackDamage                    ;              //공격최대데미지
    public float _AttackSpeed                        ;              //공격속도
    public ItemQuality _Quality                      ;              //아이템등급
    public float _ItemPrice                          ;              //아이템 가격
  
    public Item(Sprite itemImage)
    {
    
    }
    public virtual void Skill() { }
    public virtual void Attack() { }
}
public class Shortrange : Item
{
    public Shortrange( Sprite itemImage)
        :base(itemImage)
    {

    }
    public override void Skill()
    { }
    public override void Attack()
    { }


}

public class Longrange : Item
{
    public Longrange( Sprite itemImage)
          : base(itemImage)
    {

    }
    public override void Skill()
    { }
    public override void Attack()
    { }
    public virtual void Fire() { }

}