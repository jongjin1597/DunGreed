using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLongLangeMonster : cMonsterBase
{

    protected override void Awake()
    {
        base.Awake();

    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        base.MonsterHIT(dam, isCritical);
    }
    public override void DropGold()
    {
    }

}
