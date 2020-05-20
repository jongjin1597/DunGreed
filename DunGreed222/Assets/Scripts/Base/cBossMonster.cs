using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBossMonster : cMonsterBase
{
    protected cBossBullet _Anemybullet;
    protected List<cBossBullet> _BulletPoll = new List<cBossBullet>();
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
