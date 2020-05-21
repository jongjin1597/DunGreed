using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    OneShot,
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
public class Item : MonoBehaviour
{                                      
    public int _ItemID                               ;              //아이템번호
    public string _ItemName                          ;              //아이템이름
    public string _ItemDescrIption                   ;              //아이템설명
    public Sprite _ItemIcon                          ;              //아이템이미지
    public  ItemType _Type                           ;              //아이템타입
    public int _MinAttackDamage                      ;              //공격최소데미지
    public int _MaxAttackDamage                      ;              //공격최대데미지
    public float _AttackSpeed                        ;              //공격속도
    public ItemQuality _Quality                      ;              //아이템등급
    public float _ItemPrice                          ;              //아이템 가격
    public string _SkillText                         ;              //스킬설명
    public int _Dam                                  ;              //공격데미지
    public bool _Skill=true                          ;              //스킬사용가능여부
    public Sprite _SkillIcon                         ;              //스킬이미지
    public float _SkillCollTime                      ;              //스킬쿨타임   
    public virtual void Skill() { }


}
public class Shortrange : Item
{

    public override void Skill()
    { }
    public virtual void Attack(cMonsterBase Monster)
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

}
public class Longrange : Item
{
    protected List<cBullet> _BulletPoll = new List<cBullet>(); //풀에 담을

    public float _Delay;
    protected int _MaxBullet;
    protected int _CurBulletIndex = 0;     //현재 장전된 총알의 인덱스
    
    public override void Skill()
    { }

    public virtual void FireBulet(Vector2 Position, float _angle)
    {
        //발사되어야할 순번의 총알이 이전에 발사한 후로 아직 날아가고 있는 중이라면, 발사를 못하게 한다.
        if (_BulletPoll[_CurBulletIndex].gameObject.activeSelf)
        {
            return;
        }

        _BulletPoll[_CurBulletIndex].transform.position = Position;

        _BulletPoll[_CurBulletIndex].transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _BulletPoll[_CurBulletIndex]._Start = true;
        _BulletPoll[_CurBulletIndex].gameObject.SetActive(true);
        StartCoroutine("ActiveBullet", _BulletPoll[_CurBulletIndex]);
        if (_CurBulletIndex >= _MaxBullet - 1)
        {
            _CurBulletIndex = 0;
        }
        else
        {
            _CurBulletIndex++;
        }
    }
}