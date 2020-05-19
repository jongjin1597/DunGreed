using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//몬스터, 플레이어 최상단
public class cMonsterBase : cCharacter
{
   
    protected override void Awake()
    {
        base.Awake();
     }
    public override void HIT(int dam)
    {

    }
}
