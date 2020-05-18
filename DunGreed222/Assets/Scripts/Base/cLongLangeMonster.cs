using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cLongLangeMonster : cMonsterBase
{
   protected cBullet _Anemybullet;
    protected List<cBullet> _BulletPoll = new List<cBullet>();
    protected int _CurBulletIndex=0;
    protected int _MaxBullet;
    protected override void Awake()
    {
        base.Awake();

    }

}
