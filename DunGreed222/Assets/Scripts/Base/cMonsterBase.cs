using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//몬스터, 플레이어 최상단
public class cMonsterBase : cCharacter
{

    public delegate void _MonsterDie(GameObject gameObject);
    public _MonsterDie DIE;
   
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
        _HPBarBackGround = transform.GetChild(0).GetChild(0).gameObject;
        _HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        _HPBar.fillAmount = 1;
        _SmallGold = Resources.Load("Prefabs/Item/Bullion") as GameObject;
        _BigGold = Resources.Load("Prefabs/Item/GoldCoin") as GameObject;
        _Damage = Resources.Load("Prefabs/Text") as GameObject;
        GoldFower = 200;
        DIE += Die;
    }
    public virtual void MonsterHIT(int dam,bool isCritical)
    {
        _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
        _HPBarBackGround.SetActive(true);
        GameObject Dam = Instantiate(_Damage);
        Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Dam.GetComponent<cText>().SetDamage(dam, isCritical);

        if (_currnetHP > 0)
        {
            _currnetHP -= dam;
        }
        else if (_currnetHP <= 0)
        {
            DIE(this.gameObject);
        }
    }
    public virtual void Die(GameObject gameObject)
    {
        DropGold();
        Destroy(gameObject);
    }
    IEnumerator HPCourutin()
    {
        yield return new WaitForSeconds(2.0f);
        _HPBarBackGround.SetActive(false);
    }
    public virtual void DropGold()
    {
        
    }
}
