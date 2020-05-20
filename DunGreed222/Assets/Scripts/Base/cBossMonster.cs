using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBossMonster : cMonsterBase
{
    protected cBullet _Anemybullet;
    protected List<cBullet> _BulletPoll = new List<cBullet>();
    protected int _CurBulletIndex = 0;
    protected int _MaxBullet;

    protected cBossSword _BossSword;
    protected List<cBossSword> _BossSwordPoll = new List<cBossSword>();
    protected int _CurBossSwordIndex = 0;
    protected int _MaxBossSword;

    protected override void Awake()
    {
        base.Awake();

    }
}
