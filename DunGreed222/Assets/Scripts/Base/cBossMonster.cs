using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class cBossMonster : cCharacter
{
    protected cBullet _Anemybullet;
    protected List<cBullet> _BulletPoll = new List<cBullet>();
    protected int _CurBulletIndex = 0;
    protected int _MaxBullet;

    protected cBossSword _BossSword;
    protected List<cBossSword> _BossSwordPoll = new List<cBossSword>();
    protected int _CurBossSwordIndex = 0;
    protected int _MaxBossSword;

    protected GameObject _Damage;
    protected GameObject _HPBarBackGround;
    protected Image _HPBar;
    protected GameObject _SmallGold;
    protected GameObject _BigGold;
    protected Rigidbody2D _SmallGoldRigidBody;
    protected Rigidbody2D _BigGoldRigidBody;
    protected int GoldFower;
    protected int GoldX;
    protected override void Awake()
    {
        base.Awake();

        _Clip.Add(Resources.Load<AudioClip>("Sound/Hit_Monster"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/MonsterDie"));
        _HPBarBackGround = transform.GetChild(0).GetChild(0).gameObject;
        _HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        _HPBar.fillAmount = 1;
        _BigGold = Resources.Load("Prefabs/Item/Bullion") as GameObject;
        _SmallGold = Resources.Load("Prefabs/Item/GoldCoin") as GameObject;
        _Damage = Resources.Load("Prefabs/Text") as GameObject;
        GoldFower = 200;
    }
    public virtual void HIT(int dam, bool isCritical)
    {
        _Audio.clip = _Clip[0];
        _Audio.Play();
        GameObject Dam = Instantiate(_Damage);
        Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Dam.GetComponent<cText>().SetDamage(dam, isCritical);

        if (_currnetHP > 0)
        {
            _Renderer.color = new Color(1, 0, 0, 1);
            _currnetHP -= dam;
            _HPBarBackGround.SetActive(true);
            _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
            StartCoroutine(SetRed());
  
        }
        else if (_currnetHP <= 0)
        {
            Die(this.gameObject);
        }
    }
    IEnumerator SetRed()
    {
        yield return new WaitForSeconds(0.2f);
        _Renderer.color = new Color(1, 1, 1, 1);
    }
    public virtual void Die(GameObject gameObject)
    {
        DropGold();
        _Audio.clip = _Clip[1];
        _Audio.Play();
        //_Anim.SetTrigger("Die");

    }
    void SetActive()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void DropGold()
    {

    }

}
