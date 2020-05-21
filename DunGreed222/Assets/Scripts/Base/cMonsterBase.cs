using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//몬스터, 플레이어 최상단
public class cMonsterBase : cCharacter
{
    public GameObject _Damage;
    public GameObject _HPBarBackGround;
    public Image _HPBar;
    protected override void Awake()
    {
        base.Awake();
        _HPBarBackGround = transform.GetChild(0).GetChild(0).gameObject;
        _HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        _HPBar.fillAmount = 1;
     }
    public virtual void MonsterHIT(int dam,bool isCritical)
    {
        _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
        _HPBarBackGround.SetActive(true);
        GameObject Dam = Instantiate(_Damage);
        Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        Dam.GetComponent<cDamageText>().SetDamage(dam, isCritical);


        if (_currnetHP > 0)
        {
            _currnetHP -= dam;
        }
        else if (_currnetHP <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
