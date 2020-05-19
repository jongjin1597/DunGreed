using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터, 플레이어 최상단
public class cMonsterBase : cCharacter
{
    public GameObject _Damage;
    protected override void Awake()
    {
        base.Awake();
     }
    public virtual void MonsterHIT(int dam,bool isCritical)
    {

    }
}
