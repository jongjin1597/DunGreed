using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cDrop: MonoBehaviour
{
    public GameObject _NormalBox;
    public GameObject _RareBox;
    public GameObject _UniqueBox;

    public GameObject _SmallGold;
    public GameObject _BigGold;
    private Rigidbody2D _SmallGoldRigidBody;
    private Rigidbody2D _BigGoldRigidBody;
    void Awake()
    {
        
        _NormalBox= Resources.Load("Prefabs/DunguenBox/NormalBox") as GameObject;
        _RareBox= Resources.Load("Prefabs/DunguenBox/RareBox") as GameObject;
        _UniqueBox= Resources.Load("Prefabs/DunguenBox/UniqueBox") as GameObject;
        _SmallGold = Resources.Load("Prefabs/Item/Bullion") as GameObject;
        _BigGold = Resources.Load("Prefabs/Item/GoldCoin") as GameObject;
        _SmallGoldRigidBody = _SmallGold.GetComponent<Rigidbody2D>();
        _BigGoldRigidBody = _BigGold.GetComponent<Rigidbody2D>();
    }

    public void DropGold(int _Loop,int NoonGold,int SmallGold,int BigGold,Vector3 Position)
    {
        for(int i=0; i <= _Loop; ++i)
        {
            int RandomIndex = Random.Range(1, 101);
            if(RandomIndex>= NoonGold && RandomIndex <= NoonGold + SmallGold)
            {
                GameObject obj = Instantiate(_SmallGold) as GameObject;
                cSmallGold GoldCoin = obj.GetComponent<cSmallGold>();
                GoldCoin.transform.position = Position;
                _SmallGoldRigidBody.AddForce(new Vector2(0, 1));
            }
            else if (RandomIndex >= NoonGold + SmallGold && RandomIndex <= NoonGold + SmallGold+BigGold)
            {
                GameObject obj = Instantiate(_BigGold) as GameObject;
                cBigGold GoldCoin = obj.GetComponent<cBigGold>();
                GoldCoin.transform.position = Position;
                _BigGoldRigidBody.AddForce(new Vector2(0, 1));
            }
        }
    }
    public void SetBox(Vector3 Position)
    {
        int RandomIndex = Random.Range(1, 101);
        if(RandomIndex >= 51 && RandomIndex <= 80)
        {
            GameObject obj = Instantiate(_NormalBox) as GameObject;
            cBasicBox NormalBox = obj.GetComponent<cBasicBox>();
            NormalBox.transform.position = Position;
        }
        if (RandomIndex >= 81 && RandomIndex <= 95)
        {
            GameObject obj = Instantiate(_RareBox) as GameObject;
            cRareBox RareBox = obj.GetComponent<cRareBox>();
            RareBox.transform.position = Position;
        }
        if (RandomIndex >= 95 && RandomIndex <= 100)
        {
            GameObject obj = Instantiate(_UniqueBox) as GameObject;
            cUniqueBox UniqueBox = obj.GetComponent<cUniqueBox>();
            UniqueBox.transform.position = Position;
        }

    }
}
