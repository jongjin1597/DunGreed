using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cBossMonster : cMonsterBase
{

    protected cBossSword _BossSword;
    protected List<cBossSword> _BossSwordPoll = new List<cBossSword>();
    protected int _CurBossSwordIndex = 0;
    protected int _MaxBossSword;

 
    DieEffect _DieEffect;
    protected override void Awake()
    {
        base.Awake();

        _Clip.Add(Resources.Load<AudioClip>("Sound/Hit_Monster"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/MonsterDie"));
        _HPBarBackGround = transform.GetChild(0).GetChild(0).gameObject;
        _HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        _HPBar.fillAmount = 1;
        _BigGold = Resources.Load("Prefabs/Item/Bullion") as GameObject;
          _Damage = Resources.Load("Prefabs/Text") as GameObject;
        GoldFower = 200;
        _DieEffect = FindObjectOfType<DieEffect>();
    }
    public override void MonsterHIT(int dam, bool isCritical)
    {
        if (_currnetHP > 0)
        {
             _Audio.clip = _Clip[0];
             _Audio.Play();
             GameObject Dam = Instantiate(_Damage);
             Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
             Dam.GetComponent<cText>().SetDamage(dam, isCritical);
            _Renderer.color = new Color(1, 0, 0, 1);
            _currnetHP -= dam;
            _HPBarBackGround.SetActive(true);
            _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
            StartCoroutine(SetRed());
  
        }
       
    }
    IEnumerator SetRed()
    {
        yield return new WaitForSeconds(0.2f);
        _Renderer.color = new Color(1, 1, 1, 1);
    }
    public virtual void Die(GameObject gameObject)
    {
        _Audio.clip = _Clip[1];
        _Audio.Play();
        _DieEffect.Die();
        DropGold();

    }
    void SetActive()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator DropGold()
    {
        for (int i = 0; i < 20; ++i)
        {
            GameObject obj = Instantiate(_BigGold) as GameObject;
            obj.transform.position = this.transform.position;
            GoldX = Random.Range(-100, 100);
            obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoldX, GoldFower));
            yield return new WaitForSeconds(0.1f);
        }
    }

}
