using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//몬스터, 플레이어 최상단
public class cMonsterBase : cCharacter
{
    public GameObject _Damage;
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
        GoldFower = 200;

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
            DropGold();
            Destroy(this.gameObject);
        }
    }
    IEnumerator HPCourutin()
    {
        yield return new WaitForSeconds(2.0f);
        _HPBarBackGround.SetActive(false);
    }
    public virtual void DropGold()
    {
        //for (int i = 0; i <= _Loop; ++i)
        //{
        //    int RandomIndex = Random.Range(1, 101);
        //    if (RandomIndex >= NoonGold && RandomIndex <= NoonGold + SmallGold)
        //    {
        //        GameObject obj = Instantiate(_SmallGold) as GameObject;
        //        cSmallGold GoldCoin = obj.GetComponent<cSmallGold>();
        //        GoldCoin.transform.position = Position;
        //        _SmallGoldRigidBody.AddForce(new Vector2(0, 1));
        //    }
        //    else if (RandomIndex >= NoonGold + SmallGold && RandomIndex <= NoonGold + SmallGold + BigGold)
        //    {
        //        GameObject obj = Instantiate(_BigGold) as GameObject;
        //        cBigGold GoldCoin = obj.GetComponent<cBigGold>();
        //        GoldCoin.transform.position = Position;
        //        _BigGoldRigidBody.AddForce(new Vector2(0, 1));
        //    }
        //}
    }
}
