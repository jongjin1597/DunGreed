using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//몬스터, 플레이어 최상단
public class cMonsterBase : cCharacter
{


   
    protected GameObject _Damage;
    protected GameObject _HPBarBackGround;
    protected Image _HPBar;
    protected GameObject _SmallGold;
    protected GameObject _BigGold;
    protected Rigidbody2D _SmallGoldRigidBody;
    protected Rigidbody2D _BigGoldRigidBody;
    protected int GoldFower;
    protected int _AttackDamage;
    protected int GoldX;
    protected bool _isDie=false;
    protected override void Awake()
    {
        base.Awake();

        _Clip.Add(Resources.Load<AudioClip>("Sound/Hit_Monster"));
        _Clip.Add(Resources.Load<AudioClip>("Sound/MonsterDie"));
        _HPBarBackGround = transform.GetChild(0).GetChild(0).gameObject;
        _HPBar = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        _HPBar.fillAmount = 1;
        _BigGold= Resources.Load("Prefabs/Item/Bullion") as GameObject;
        _SmallGold = Resources.Load("Prefabs/Item/GoldCoin") as GameObject;
        _Damage = Resources.Load("Prefabs/Text") as GameObject;
        GoldFower = 200;
       
    }
    public virtual void MonsterHIT(int dam,bool isCritical)
    {

        if (_currnetHP > 0)
        {
            _Audio.clip = _Clip[0];
             _Audio.Play();
               GameObject Dam = Instantiate(_Damage);
             Dam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            Dam.GetComponent<cText>().SetDamage(dam, isCritical);
            _currnetHP -= dam;
            _HPBarBackGround.SetActive(true);
            _HPBar.fillAmount = (float)_currnetHP / _MaxHP;
            CancelInvoke();
            //StopAllCoroutines();
            Invoke("ActiveHP", 3f);
        }


    }

   

   
    public virtual void Die(GameObject gameObject)
    {
        cMapManager.GetInstance.ReMoveMonster(gameObject);
        DropGold();
        _Audio.clip = _Clip[1];
        _Audio.Play();
        _Anim.SetTrigger("Die");

    }
    void SetActive()
    {

        Destroy(this.gameObject);
    }
    IEnumerator HPCourutin()
    {
        yield return new WaitForSeconds(2.0f);
        _HPBarBackGround.SetActive(false);
    }
    public virtual void DropGold()
    {
        
    }
    void ActiveHP()
    {
       // yield return new WaitForSeconds(3.0f);
        _HPBarBackGround.SetActive(false);
    }
}
